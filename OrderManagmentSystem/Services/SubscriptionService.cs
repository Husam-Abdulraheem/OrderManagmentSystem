using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Data.Models;
using OrderManagementSystem.Interfaces;

namespace OrderManagementSystem.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ApplicationDbContext _db;

        public SubscriptionService(ApplicationDbContext db)
        {
            _db = db;
        }


        // Is Active Subscription
        public async Task<object> IsActive(int supplierId)
        {
            var supplier = await _db.Suppliers.Where(s => s.Id == supplierId).Include(s => s.Subscription).FirstOrDefaultAsync();
            if (supplier == null)
                return new { Message = "no supplier with this id " + supplier.Id };

            if (supplier.Subscription == null)
            {
                return new { Message = "No Subscription" };
            }

            var TimeLeft = (supplier.Subscription.EndDate.Date - DateTime.UtcNow.Date).TotalDays;

            if (TimeLeft <= 0)
            {
                supplier.Subscription.IsActive = false;
                _db.Subscriptions.Update(supplier.Subscription);
                await _db.SaveChangesAsync();

                return new { Exp = supplier.Subscription.EndDate, state = false, timeLeft = 0, type = supplier.Subscription.Type };
            }

            return new { Exp = supplier.Subscription.EndDate, state = supplier.Subscription.IsActive, timeLeft = TimeLeft, type = supplier.Subscription.Type };
        }



        // Update subscription
        public async Task<object> UpdateSubscription(string supplierEmail)
        {
            var supplier = await _db.Suppliers.Where(e => e.User.Email == supplierEmail).Include(e => e.Subscription).FirstOrDefaultAsync();

            if (supplier == null)
            {
                throw new Exception("Not found this Supplier");
            }

            if (supplier.Subscription == null)
            {
                throw new Exception("This Supplier has no existing Subscription");
            }

            if (supplier.Subscription.IsActive)
            {
                return new { Message = "This Supplier already has an active Subscription" };
            }

            var subType = supplier.Subscription.Type;

            supplier.Subscription.StartDate = DateTime.UtcNow;
            supplier.Subscription.EndDate =
                subType.ToLower() == "monthly" ? DateTime.UtcNow.AddMonths(1) :
                subType.ToLower() == "Quarterly" ? DateTime.UtcNow.AddMonths(3) :
                subType.ToLower() == "biannual" ? DateTime.UtcNow.AddMonths(6) :
                DateTime.UtcNow.AddYears(1);
            supplier.Subscription.IsActive = true;

            _db.Subscriptions.Update(supplier.Subscription);
            await _db.SaveChangesAsync();

            return new { Message = "Update you subscription successfully" };

        }


        // Add new Subscription
        public async Task<object> AddNewSubscription(string supplierEmail, string type)
        {
            var supplier = await _db.Suppliers
        .Include(s => s.Subscription)
        .FirstOrDefaultAsync(s => s.User.Email == supplierEmail);

            if (supplier == null)
                return new { Message = "Not found this User" };

            if (supplier.Subscription != null)
            {
                return new { Message = "You already have Subscription" };
            }

            if (type.ToLower() != "monthly" && type.ToLower() != "quarterly" &&
                type.ToLower() != "biannual" && type.ToLower() != "yearly")
            {
                throw new ArgumentException("Invalid subscription type");
            }

            var subscription = new Subscription
            {
                SupplierId = supplier.Id,
                StartDate = DateTime.UtcNow,
                EndDate =
            type.ToLower() == "monthly" ? DateTime.UtcNow.AddMonths(1) :
            type.ToLower() == "Quarterly" ? DateTime.UtcNow.AddMonths(3) :
            type.ToLower() == "biannual" ? DateTime.UtcNow.AddMonths(6) :
            type.ToLower() == "yearly" ? DateTime.UtcNow.AddYears(1) :
            DateTime.UtcNow,

                IsActive = true,
                Type = type
            };

            await _db.Subscriptions.AddAsync(subscription);
            await _db.SaveChangesAsync();
            return new { Message = "Added new Subscription successfully" };

        }


        public async Task<object> DeleteSubscription(string email)
        {
            var supplier = await _db.Suppliers
                .Where(s => s.User.Email == email)
                .Include(s => s.Subscription)
                .FirstOrDefaultAsync();

            if (supplier == null)
                return new { Message = "Not Found this Supplier" };

            if (supplier.Subscription == null)
                return new { Message = "This Supplier does not have Subscription" };

            _db.Subscriptions.Remove(supplier.Subscription);
            await _db.SaveChangesAsync();
            return new { Message = "Deleted" };
        }
    }
}

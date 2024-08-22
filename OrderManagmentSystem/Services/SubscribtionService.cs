using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Data.Models;
using OrderManagementSystem.Interfaces;

namespace OrderManagementSystem.Services
{
    public class SubscribtionService : ISubscribtionService
    {
        private readonly ApplicationDbContext _db;

        public SubscribtionService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<object> IsActive(int supplierId)
        {
            var subscription = await _db.Subscriptions
            .Where(s => s.SupplierId == supplierId && s.IsActive)
            .OrderByDescending(s => s.EndDate)
            .FirstOrDefaultAsync();
            if (subscription == null || subscription.EndDate < DateTime.UtcNow)
            {
                return new { Message = "No Subscribtion" };
            }

            return new { Exp = subscription.EndDate, state = true };
        }

        public async Task RenewSubscription(int supplierId, string type)
        {

            var subscribtion = new Subscription
            {
                SupplierId = supplierId,
                StartDate = DateTime.UtcNow,
                EndDate =
                type.ToLower() == "month" ? DateTime.UtcNow.AddMonths(1) :
                type.ToLower() == "threemonth" ? DateTime.UtcNow.AddMonths(3) :
                type.ToLower() == "sexmonth" ? DateTime.UtcNow.AddMonths(6) :
                DateTime.UtcNow.AddYears(1),
                IsActive = true,
                Type = type
            };
            await _db.Subscriptions.AddAsync(subscribtion);
            var supplier = await _db.Suppliers.FindAsync(supplierId);

            if (supplier != null)
            {
                supplier.Subscription = subscribtion;
                _db.Suppliers.Update(supplier);
            }

            await _db.SaveChangesAsync();
        }
    }
}

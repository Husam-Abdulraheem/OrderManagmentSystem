namespace OrderManagementSystem.Interfaces
{
    public interface ISubscriptionService
    {
        Task<object> IsActive(int supplierId);
        Task<object> UpdateSubscription(string Email);
        Task<object> AddNewSubscription(string Email, string type);
        Task<object> DeleteSubscription(string Email);
    }
}

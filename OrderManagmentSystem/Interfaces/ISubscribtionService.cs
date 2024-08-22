namespace OrderManagementSystem.Interfaces
{
    public interface ISubscribtionService
    {
        Task<object> IsActive(int supplierId);
        Task RenewSubscription(int supplierId, string type);
    }
}

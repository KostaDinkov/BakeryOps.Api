using Orders.Models;

namespace Orders.Repositories
{
    public interface IOrdersRepository
    {
        public Task<IEnumerable<Order>> GetOrdersBetween (DateTime startDate, DateTime endDate);

        public Task<IEnumerable<Order>> GetOrdersForDate (DateTime date);
        public Task<Order> GetOrderById (int id);
        public Task AddOrder (Order order);
        public Task DeleteOrder (int id);
        public void UpdateOrder (Order order);

        public Task Save();


    }
}

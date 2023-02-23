using Orders.Models;
using Orders.Models.DTOs;

namespace Orders.Services
{
    public interface IOrdersService
    {
        Task<int> AddOrder(OrderDTO orderDto);
        Task DeleteOrder(int id);
        Task<Order> GetOrderById(int id);
        Task<IEnumerable<Order>> GetOrdersBetween(DateTime startDate, DateTime endDate);
        Task UpdateOrder(int id, OrderDTO update);
    }
}
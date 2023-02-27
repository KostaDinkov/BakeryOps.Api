using Orders.Models;
using Orders.Models.DTOs;

namespace Orders.Services
{
    public interface IOrdersService
    {
        Task<Order> AddOrder(OrderDTO orderDto);
        Task<Order> DeleteOrder(int id);
        Task<Order> GetOrderById(int id);
        Task<IEnumerable<Order>> GetOrdersBetween(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Order>> GetOrdersFor(DateTime startDate);
        Task<Order> UpdateOrder(int id, OrderDTO update);
    }
}
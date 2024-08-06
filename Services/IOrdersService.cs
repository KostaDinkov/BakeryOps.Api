using BakeryOps.API.Models;
using BakeryOps.API.Models.DTOs;

namespace BakeryOps.API.Services
{
    public interface IOrdersService
    {
        Task<Order> AddOrder(OrderDTO orderDto);
        Task<Order> DeleteOrder(int id);
        Task<Order> GetOrderById(int id);
        Task<List<List<Order>>> GetOrdersBetween(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Order>> GetOrdersFor(DateTime startDate);
        Task<Order> UpdateOrder(int id, OrderDTO update);
    }
}
using BakeryOps.API.Data;
using BakeryOps.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BakeryOps.API.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {

        private readonly AppDb dbContext;
        public OrdersRepository(AppDb dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddOrder(Order order)
        {
            await dbContext.Orders.AddAsync(order);
        }

        public async Task DeleteOrder(int id)
        {
            Order order = await dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return;
            }
            dbContext.Orders.Remove(order);
        }

        public async Task<Order> GetOrderById(int id)
        {
            Order order = await dbContext.Orders.FindAsync(id);
            return order;
        }

        public async Task<IEnumerable<Order>> GetOrdersBetween(DateTime startDate, DateTime endDate)
        {
            var results = await dbContext.Orders.Where(o=> o.PickupDate >= startDate && o.PickupDate <= endDate).ToListAsync();
            return results;
        }

        public async Task<IEnumerable<Order>> GetOrdersForDate(DateTime date)
        {
            var order = await dbContext.Orders.Where(o => o.PickupDate.Date == date.Date).ToArrayAsync();
            return order;
        }

        public async Task Save()
        {
            await dbContext.SaveChangesAsync();
        }

        public void UpdateOrder(Order order)
        {
            dbContext.Entry(order).State = EntityState.Modified;
        }
    }
}

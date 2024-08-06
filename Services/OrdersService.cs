using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orders.Data;
using Orders.Models;
using Orders.Models.DTOs;
using Orders.Repositories;

namespace Orders.Services
{
    public class OrdersService : IOrdersService
    {

        private readonly AppDb dbContext;
        private readonly IMapper mapper;
        private readonly ILogger logger;
        public OrdersService(AppDb appDb, IMapper mapper, ILogger<OrdersService> logger)
        {

            this.mapper = mapper;
            this.dbContext = appDb;
            this.logger = logger;
        }
        public async Task<Order> GetOrderById(int id)
        {
            return await dbContext.Orders.FindAsync(id);

        }

        public async Task<List<List<Order>>> GetOrdersBetween(DateTime startDate, DateTime endDate)
        {
            var orders = await dbContext.Orders
                .Where(o => o.PickupDate.Date >= startDate && o.PickupDate.Date <= endDate)
                .GroupBy(o => o.PickupDate.Date)
                .OrderBy(g=>g.Key)
                .Select(g => g.OrderBy(o => o.PickupDate.TimeOfDay).ToList())
                .ToListAsync();
            
            logger.Log(LogLevel.Information, "Orders Requested");
            
                
            return orders;
        }
        public async Task<IEnumerable<Order>> GetOrdersFor(DateTime startDate)
        {
            var orders = await dbContext.Orders.Where(o => o.PickupDate.Date == startDate.Date).ToListAsync();
            return orders;
        }

        public async Task<Order> AddOrder(OrderDTO orderDto)
        {

            //For new order, there should be no nulls            
            var order = mapper.Map<OrderDTO, Order>(orderDto);
            if (orderDto.ClientId is not null)
            {
                Client client = await dbContext.Clients.FindAsync(orderDto.ClientId);
                order.Client = client;
                //client.Orders.Add(order);
                order.ClientName = client.Name;
                //order.ClientPhone = client.Phone;
            }

            await dbContext.Orders.AddAsync(order);
            dbContext.SaveChanges();
            return order;
        }

        public async Task<Order> DeleteOrder(int id)
        {
            var order = await dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                throw new InvalidOperationException();
            }

            dbContext.Orders.Remove(order);
            await dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateOrder(int id, OrderDTO update)
        {
            var order = await dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                throw new InvalidOperationException();
            }
            mapper.Map(update, order);
            await dbContext.SaveChangesAsync();
            return order;

        }

    }
}

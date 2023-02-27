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

        private readonly OrdersDB dbContext;
        private readonly IMapper mapper;
        public OrdersService(OrdersDB ordersDb, IMapper mapper)
        {

            this.mapper = mapper;
            this.dbContext = ordersDb;
        }
        public async Task<Order> GetOrderById(int id)
        {
            return await dbContext.Orders.FindAsync(id);

        }

        public async Task<IEnumerable<Order>> GetOrdersBetween(DateTime startDate, DateTime endDate)
        {

            var orders = await dbContext.Orders.Where(o => o.PickupDate >= startDate && o.PickupDate <= endDate).ToListAsync();
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
                client.Orders.Add(order);
                order.ClientName = client.Name;
                order.ClientPhone = client.Phone;
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

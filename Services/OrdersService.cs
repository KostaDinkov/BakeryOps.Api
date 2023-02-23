using AutoMapper;
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
        public OrdersService( OrdersDB ordersDb, IMapper mapper)
        {
            
            this.mapper = mapper;
            this.dbContext = ordersDb;  
        }
        public async Task<Order> GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetOrdersBetween(DateTime startDate, DateTime endDate)
        {

            throw new NotImplementedException();
        }

        public async Task<int> AddOrder(OrderDTO orderDto)
        {

            //For new order, there should be no nulls
            
            var order = mapper.Map<OrderDTO, Order>(orderDto);
            if (orderDto.ClientId is not null)
            {
                Client client = await dbContext.Clients.FindAsync(orderDto.ClientId);
                order.Client = client;
                client.Orders.Add(order);
            }

            await dbContext.Orders.AddAsync(order);
            dbContext.SaveChanges();            
            return order.Id;
        }

        public async Task DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateOrder(int id, OrderDTO update)
        {
            throw new NotImplementedException();
            
        }

    }
}

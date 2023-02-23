using AutoMapper;
using Orders.API.Models.DTOs;
using Orders.Models.DTOs;

namespace Orders.Models.AutoMapperProfiles
{
    public class OrderProfile: Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDTO>().ReverseMap();           
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<Client, ClientDTO>().ReverseMap();
            
        }
    }

    
}

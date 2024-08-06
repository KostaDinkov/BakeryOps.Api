using AutoMapper;
using BakeryOps.API.Models.DTOs;

namespace BakeryOps.API.Models.AutoMapperProfiles
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

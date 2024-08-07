using AutoMapper;
using BakeryOps.API.Models.DTOs;

namespace BakeryOps.API.Models.AutoMapperProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<Client, ClientDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ForMember(dest => dest.Permissions,
                opt =>
                    opt.MapFrom(src => src.Permissions.Select(p => p.Name).ToArray())).ReverseMap();
        }
    }
}
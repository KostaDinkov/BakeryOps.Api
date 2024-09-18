using AutoMapper;
using BakeryOps.API.Models.DTOs;

namespace BakeryOps.API.Models.AutoMapperProfiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<Client, ClientDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ForMember(dest => dest.Permissions,
                opt =>
                    opt.MapFrom(src => src.Permissions.Select(p => p.Name).ToArray())).ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Vendor, VendorDTO>().ReverseMap();
            
            CreateMap<Material, MaterialDTO>()
                .ForMember(dest=> dest.CategoryId, opt =>
                {
                    opt.MapFrom(src=>src.Category.Id);
                })
                .ForMember(dest => dest.VendorId, opt =>
                {
                    opt.MapFrom(src => src.Vendor.Id);
                })
                .ReverseMap()
                .ForPath(dest => dest.CategoryId, 
                    opt => opt.MapFrom(src => src.CategoryId))
                .ForPath(dest=>dest.VendorId, 
                    opt=>opt.MapFrom(src=>src.VendorId));



        }
    }
}
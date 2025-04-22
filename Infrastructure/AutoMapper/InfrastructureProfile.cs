using AutoMapper;
using Domain.DTOs.Couriers;
using Domain.DTOs.Menues;
using Domain.DTOs.OrderDetails;
using Domain.DTOs.Orders;
using Domain.DTOs.Restauronts;
using Domain.DTOs.Users;
using Domain.Entities;

namespace Infrastructure.AutoMapper;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<User, GetUserDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>();
        CreateMap<Restaurant, GetRestaurantDto>();
        CreateMap<Courier, GetCourierDto>();
        CreateMap<Order, GetOrderDto>();
        CreateMap<OrderDetail, GetOrderDetailDto>();
        CreateMap<Menu, GetMenuDto>();

    }

}

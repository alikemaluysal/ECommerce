using Application.Features.Orders.Commands.Create;
using Application.Features.Orders.Commands.Delete;
using Application.Features.Orders.Commands.Update;
using Application.Features.Orders.Commands.Checkout;
using Application.Features.Orders.Commands.UpdateOrderStatus;
using Application.Features.Orders.Queries.GetById;
using Application.Features.Orders.Queries.GetList;
using Application.Features.Orders.Queries.GetUserOrders;
using Application.Features.Orders.Queries.GetAllOrders;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Orders.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateOrderCommand, Order>();
        CreateMap<Order, CreatedOrderResponse>();

        CreateMap<UpdateOrderCommand, Order>();
        CreateMap<Order, UpdatedOrderResponse>();

        CreateMap<DeleteOrderCommand, Order>();
        CreateMap<Order, DeletedOrderResponse>();

        CreateMap<Order, GetByIdOrderResponse>();

        CreateMap<Order, GetListOrderListItemDto>();
        CreateMap<IPaginate<Order>, GetListResponse<GetListOrderListItemDto>>();

        CreateMap<Order, CheckedOutResponse>();

        CreateMap<Order, UpdatedOrderStatusResponse>();

        CreateMap<Order, GetUserOrdersListItemDto>()
            .ForMember(dest => dest.TotalItems, opt => opt.MapFrom(src => src.OrderItems.Sum(oi => oi.Quantity)));
        CreateMap<IPaginate<Order>, GetListResponse<GetUserOrdersListItemDto>>();

        CreateMap<Order, GetAllOrdersListItemDto>()
            .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.TotalItems, opt => opt.MapFrom(src => src.OrderItems.Sum(oi => oi.Quantity)));
        CreateMap<IPaginate<Order>, GetListResponse<GetAllOrdersListItemDto>>();
    }
}
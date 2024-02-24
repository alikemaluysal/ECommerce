using Application.Features.Baskets.Commands.Create;
using Application.Features.Baskets.Commands.Delete;
using Application.Features.Baskets.Commands.Update;
using Application.Features.Baskets.Queries.GetById;
using Application.Features.Baskets.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Baskets.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Basket, CreateBasketCommand>().ReverseMap();
        CreateMap<Basket, CreatedBasketResponse>().ReverseMap();
        CreateMap<Basket, UpdateBasketCommand>().ReverseMap();
        CreateMap<Basket, UpdatedBasketResponse>().ReverseMap();
        CreateMap<Basket, DeleteBasketCommand>().ReverseMap();
        CreateMap<Basket, DeletedBasketResponse>().ReverseMap();
        CreateMap<Basket, GetByIdBasketResponse>().ReverseMap();
        CreateMap<Basket, GetListBasketListItemDto>().ReverseMap();
        CreateMap<IPaginate<Basket>, GetListResponse<GetListBasketListItemDto>>().ReverseMap();
    }
}
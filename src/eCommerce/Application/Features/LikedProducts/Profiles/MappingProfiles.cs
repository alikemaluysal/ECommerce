using Application.Features.LikedProducts.Commands.Create;
using Application.Features.LikedProducts.Commands.Delete;
using Application.Features.LikedProducts.Commands.Update;
using Application.Features.LikedProducts.Queries.GetById;
using Application.Features.LikedProducts.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.LikedProducts.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<LikedProduct, CreateLikedProductCommand>().ReverseMap();
        CreateMap<LikedProduct, CreatedLikedProductResponse>().ReverseMap();
        CreateMap<LikedProduct, UpdateLikedProductCommand>().ReverseMap();
        CreateMap<LikedProduct, UpdatedLikedProductResponse>().ReverseMap();
        CreateMap<LikedProduct, DeleteLikedProductCommand>().ReverseMap();
        CreateMap<LikedProduct, DeletedLikedProductResponse>().ReverseMap();
        CreateMap<LikedProduct, GetByIdLikedProductResponse>().ReverseMap();
        CreateMap<LikedProduct, GetListLikedProductListItemDto>().ReverseMap();
        CreateMap<IPaginate<LikedProduct>, GetListResponse<GetListLikedProductListItemDto>>().ReverseMap();
    }
}
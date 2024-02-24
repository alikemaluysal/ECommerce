using Application.Features.ProductImages.Commands.Create;
using Application.Features.ProductImages.Commands.Delete;
using Application.Features.ProductImages.Commands.Update;
using Application.Features.ProductImages.Queries.GetById;
using Application.Features.ProductImages.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.ProductImages.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ProductImage, CreateProductImageCommand>().ReverseMap();
        CreateMap<ProductImage, CreatedProductImageResponse>().ReverseMap();
        CreateMap<ProductImage, UpdateProductImageCommand>().ReverseMap();
        CreateMap<ProductImage, UpdatedProductImageResponse>().ReverseMap();
        CreateMap<ProductImage, DeleteProductImageCommand>().ReverseMap();
        CreateMap<ProductImage, DeletedProductImageResponse>().ReverseMap();
        CreateMap<ProductImage, GetByIdProductImageResponse>().ReverseMap();
        CreateMap<ProductImage, GetListProductImageListItemDto>().ReverseMap();
        CreateMap<IPaginate<ProductImage>, GetListResponse<GetListProductImageListItemDto>>().ReverseMap();
    }
}
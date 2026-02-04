using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Commands.AddProductImage;
using Application.Features.Products.Commands.DeleteProductImage;
using Application.Features.Products.Commands.SetPrimaryImage;
using Application.Features.Products.Commands.AddProductSpecification;
using Application.Features.Products.Commands.UpdateProductSpecification;
using Application.Features.Products.Commands.DeleteProductSpecification;
using Application.Features.Products.Queries.GetById;
using Application.Features.Products.Queries.GetList;
using Application.Features.Products.Queries.GetListByDynamic;
using Application.Features.Products.Queries.GetProductsByCategory;
using Application.Features.Products.Queries.GetProductSpecifications;
using Application.Features.Products.Queries.Search;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Products.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateProductCommand, Product>();
        CreateMap<Product, CreatedProductResponse>()
            .ForMember(dest => dest.PrimaryImageUrl, opt => opt.MapFrom(src => src.Images.FirstOrDefault(i => i.IsPrimary) != null ? src.Images.FirstOrDefault(i => i.IsPrimary)!.ImageUrl : null));

        CreateMap<UpdateProductRequest, Product>();
        CreateMap<Product, UpdatedProductResponse>()
            .ForMember(dest => dest.PrimaryImageUrl, opt => opt.MapFrom(src => src.Images.FirstOrDefault(i => i.IsPrimary) != null ? src.Images.FirstOrDefault(i => i.IsPrimary)!.ImageUrl : null));

        CreateMap<DeleteProductCommand, Product>();
        CreateMap<Product, DeletedProductResponse>();

        CreateMap<Product, GetByIdProductResponse>()
            .ForMember(dest => dest.PrimaryImageUrl, opt => opt.MapFrom(src => src.Images.FirstOrDefault(i => i.IsPrimary) != null ? src.Images.FirstOrDefault(i => i.IsPrimary)!.ImageUrl : null))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
            .ForMember(dest => dest.Specifications, opt => opt.MapFrom(src => src.Specifications));

        CreateMap<ProductImage, ProductImageDto>();
        CreateMap<ProductSpecification, ProductSpecificationDto>();

        CreateMap<Product, GetListProductListItemDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.PrimaryImageUrl, opt => opt.MapFrom(src => src.Images.FirstOrDefault(i => i.IsPrimary) != null ? src.Images.FirstOrDefault(i => i.IsPrimary)!.ImageUrl : null));
        CreateMap<IPaginate<Product>, GetListResponse<GetListProductListItemDto>>();

        CreateMap<Product, GetListByDynamicProductListItemDto>()
            .ForMember(dest => dest.PrimaryImageUrl, opt => opt.MapFrom(src => src.Images.FirstOrDefault(i => i.IsPrimary) != null ? src.Images.FirstOrDefault(i => i.IsPrimary)!.ImageUrl : null));
        CreateMap<IPaginate<Product>, GetListResponse<GetListByDynamicProductListItemDto>>();

        CreateMap<ProductImage, AddedProductImageResponse>();
        CreateMap<ProductImage, DeletedProductImageResponse>();
        CreateMap<ProductImage, SetPrimaryImageResponse>();

        CreateMap<ProductSpecification, AddedProductSpecificationResponse>();
        CreateMap<ProductSpecification, UpdatedProductSpecificationResponse>();
        CreateMap<ProductSpecification, DeletedProductSpecificationResponse>();
        CreateMap<ProductSpecification, GetProductSpecificationsResponse>();

        CreateMap<Product, GetProductsByCategoryResponse>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.PrimaryImageUrl, opt => opt.MapFrom(src => src.Images.FirstOrDefault(i => i.IsPrimary) != null ? src.Images.FirstOrDefault(i => i.IsPrimary)!.ImageUrl : null));

        CreateMap<Product, SearchProductsListItemDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.PrimaryImageUrl, opt => opt.MapFrom(src => src.Images.FirstOrDefault(i => i.IsPrimary) != null ? src.Images.FirstOrDefault(i => i.IsPrimary)!.ImageUrl : null));
        CreateMap<IPaginate<Product>, GetListResponse<SearchProductsListItemDto>>();
    }
}

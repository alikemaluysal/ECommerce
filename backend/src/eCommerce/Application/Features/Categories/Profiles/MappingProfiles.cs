using Application.Features.Categories.Commands.Create;
using Application.Features.Categories.Commands.Delete;
using Application.Features.Categories.Commands.Update;
using Application.Features.Categories.Queries.GetById;
using Application.Features.Categories.Queries.GetList;
using Application.Features.Categories.Queries.GetTopCategories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Categories.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateCategoryCommand, Category>();
        CreateMap<Category, CreatedCategoryResponse>();

        CreateMap<UpdateCategoryRequest, Category>();
        CreateMap<Category, UpdatedCategoryResponse>();

        CreateMap<DeleteCategoryCommand, Category>();
        CreateMap<Category, DeletedCategoryResponse>();

        CreateMap<Category, GetByIdCategoryResponse>();

        CreateMap<Category, GetListCategoryListItemDto>();
        CreateMap<IPaginate<Category>, GetListResponse<GetListCategoryListItemDto>>();

        CreateMap<Category, GetTopCategoriesResponse>()
            .ForMember(d => d.ProductCount, opt => opt.MapFrom(s => s.Products.Count));

    }
}
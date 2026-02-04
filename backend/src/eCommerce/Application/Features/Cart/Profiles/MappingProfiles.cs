using Application.Features.Cart.Commands.AddToCart;
using Application.Features.Cart.Commands.UpdateCartItem;
using Application.Features.Cart.Commands.RemoveFromCart;
using Application.Features.Cart.Queries.GetCart;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Cart.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CartItem, AddedToCartResponse>();
        CreateMap<CartItem, UpdatedCartItemResponse>();
        CreateMap<CartItem, RemovedFromCartResponse>();

        CreateMap<CartItem, CartItemDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.ProductImageUrl, opt => opt.MapFrom(src => 
                src.Product.Images.FirstOrDefault(i => i.IsPrimary) != null 
                    ? src.Product.Images.FirstOrDefault(i => i.IsPrimary)!.ImageUrl 
                    : null))
            .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src => src.Product.Price * src.Quantity));
    }
}

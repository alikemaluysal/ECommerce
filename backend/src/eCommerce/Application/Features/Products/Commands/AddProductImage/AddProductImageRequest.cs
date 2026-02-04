using Microsoft.AspNetCore.Http;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Products.Commands.AddProductImage;

public class AddProductImageRequest : IDto
{
    public required IFormFile ImageFile { get; set; }
    public int DisplayOrder { get; set; }
}

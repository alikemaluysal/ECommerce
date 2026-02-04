
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Products.Commands.Update;

public class UpdateProductRequest : IDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required int Stock { get; set; }
    public required Guid CategoryId { get; set; }
}

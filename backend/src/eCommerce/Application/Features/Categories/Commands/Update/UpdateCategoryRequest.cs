
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Categories.Commands.Update;

public class UpdateCategoryRequest : IDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }

}

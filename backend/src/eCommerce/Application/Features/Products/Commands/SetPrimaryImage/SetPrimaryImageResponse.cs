using NArchitecture.Core.Application.Responses;

namespace Application.Features.Products.Commands.SetPrimaryImage;

public class SetPrimaryImageResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }
}

using FluentValidation;

namespace Application.Features.LikedProducts.Commands.Create;

public class CreateLikedProductCommandValidator : AbstractValidator<CreateLikedProductCommand>
{
    public CreateLikedProductCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.ProductId).NotEmpty();
    }
}
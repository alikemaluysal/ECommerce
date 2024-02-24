using FluentValidation;

namespace Application.Features.LikedProducts.Commands.Update;

public class UpdateLikedProductCommandValidator : AbstractValidator<UpdateLikedProductCommand>
{
    public UpdateLikedProductCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.ProductId).NotEmpty();
    }
}
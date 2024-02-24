using FluentValidation;

namespace Application.Features.LikedProducts.Commands.Delete;

public class DeleteLikedProductCommandValidator : AbstractValidator<DeleteLikedProductCommand>
{
    public DeleteLikedProductCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
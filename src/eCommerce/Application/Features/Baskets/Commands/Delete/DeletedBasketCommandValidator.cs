using FluentValidation;

namespace Application.Features.Baskets.Commands.Delete;

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
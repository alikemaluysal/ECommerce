using FluentValidation;

namespace Application.Features.Baskets.Commands.Create;

public class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
{
    public CreateBasketCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.TotalAmount).NotEmpty();
    }
}
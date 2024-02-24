using FluentValidation;

namespace Application.Features.BasketItems.Commands.Create;

public class CreateBasketItemCommandValidator : AbstractValidator<CreateBasketItemCommand>
{
    public CreateBasketItemCommandValidator()
    {
        RuleFor(c => c.BasketId).NotEmpty();
        RuleFor(c => c.ProductId).NotEmpty();
        RuleFor(c => c.Quantity).NotEmpty();
        RuleFor(c => c.Price).NotEmpty();
    }
}
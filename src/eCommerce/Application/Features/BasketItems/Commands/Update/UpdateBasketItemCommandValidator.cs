using FluentValidation;

namespace Application.Features.BasketItems.Commands.Update;

public class UpdateBasketItemCommandValidator : AbstractValidator<UpdateBasketItemCommand>
{
    public UpdateBasketItemCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.BasketId).NotEmpty();
        RuleFor(c => c.ProductId).NotEmpty();
        RuleFor(c => c.Quantity).NotEmpty();
        RuleFor(c => c.Price).NotEmpty();
    }
}
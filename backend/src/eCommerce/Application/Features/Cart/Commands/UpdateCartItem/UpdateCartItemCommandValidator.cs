using FluentValidation;

namespace Application.Features.Cart.Commands.UpdateCartItem;

public class UpdateCartItemCommandValidator : AbstractValidator<UpdateCartItemCommand>
{
    public UpdateCartItemCommandValidator()
    {
        RuleFor(c => c.ItemId).NotEmpty();
        RuleFor(c => c.Dto).NotNull();
        RuleFor(c => c.Dto.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0");
    }
}

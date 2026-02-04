using FluentValidation;

namespace Application.Features.Orders.Commands.Checkout;

public class CheckoutCommandValidator : AbstractValidator<CheckoutCommand>
{
    public CheckoutCommandValidator()
    {
        RuleFor(c => c.ShippingAddress).NotEmpty().MinimumLength(5);
        RuleFor(c => c.ShippingCity).NotEmpty().MinimumLength(2);
        RuleFor(c => c.ShippingCountry).NotEmpty().MinimumLength(2);
        RuleFor(c => c.ShippingPostalCode).NotEmpty();
    }
}

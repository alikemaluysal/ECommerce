using FluentValidation;

namespace Application.Features.Orders.Commands.Update;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.TotalAmount).NotEmpty();
        RuleFor(c => c.Status).NotEmpty();
        RuleFor(c => c.ShippingAddress).NotEmpty();
        RuleFor(c => c.ShippingCity).NotEmpty();
        RuleFor(c => c.ShippingCountry).NotEmpty();
        RuleFor(c => c.ShippingPostalCode).NotEmpty();
    }
}
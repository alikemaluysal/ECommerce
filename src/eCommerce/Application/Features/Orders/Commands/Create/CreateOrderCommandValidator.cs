using FluentValidation;

namespace Application.Features.Orders.Commands.Create;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.OrderDate).NotEmpty();
        RuleFor(c => c.TotalAmount).NotEmpty();
    }
}
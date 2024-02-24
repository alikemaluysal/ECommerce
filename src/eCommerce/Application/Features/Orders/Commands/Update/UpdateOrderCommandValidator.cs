using FluentValidation;

namespace Application.Features.Orders.Commands.Update;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.OrderDate).NotEmpty();
        RuleFor(c => c.TotalAmount).NotEmpty();
    }
}
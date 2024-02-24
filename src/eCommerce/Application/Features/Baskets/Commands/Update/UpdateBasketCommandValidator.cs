using FluentValidation;

namespace Application.Features.Baskets.Commands.Update;

public class UpdateBasketCommandValidator : AbstractValidator<UpdateBasketCommand>
{
    public UpdateBasketCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.TotalAmount).NotEmpty();
    }
}
using FluentValidation;

namespace Application.Features.Products.Commands.Update;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.Price).NotEmpty();
        RuleFor(c => c.QuantityAvailable).NotEmpty();
        RuleFor(c => c.CategoryId).NotEmpty();
    }
}
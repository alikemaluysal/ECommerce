using FluentValidation;

namespace Application.Features.Products.Commands.Update;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Dto).NotNull();
        RuleFor(c => c.Dto.Name).NotEmpty();
        RuleFor(c => c.Dto.Description).NotEmpty();
        RuleFor(c => c.Dto.Price).NotEmpty().GreaterThan(0);
        RuleFor(c => c.Dto.Stock).NotEmpty().GreaterThanOrEqualTo(0);
        RuleFor(c => c.Dto.CategoryId).NotEmpty();
    }
}
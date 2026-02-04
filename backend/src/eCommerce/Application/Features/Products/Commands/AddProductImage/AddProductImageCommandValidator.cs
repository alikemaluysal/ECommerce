using FluentValidation;

namespace Application.Features.Products.Commands.AddProductImage;

public class AddProductImageCommandValidator : AbstractValidator<AddProductImageCommand>
{
    public AddProductImageCommandValidator()
    {
        RuleFor(c => c.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required");

        RuleFor(c => c.Dto).NotNull();

        RuleFor(c => c.Dto.ImageFile)
            .NotNull()
            .WithMessage("Image file is required");

        RuleFor(c => c.Dto.ImageFile.Length)
            .LessThanOrEqualTo(5 * 1024 * 1024) // 5MB
            .When(c => c.Dto.ImageFile != null)
            .WithMessage("Image file size must not exceed 5MB");

        RuleFor(c => c.Dto.DisplayOrder)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Display order must be greater than or equal to 0");
    }
}

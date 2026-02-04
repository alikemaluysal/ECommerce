using FluentValidation;

namespace Application.Features.Products.Queries.Search;

public class SearchProductsQueryValidator : AbstractValidator<SearchProductsQuery>
{
    public SearchProductsQueryValidator()
    {
        RuleFor(q => q.Request).NotNull();

        RuleFor(q => q.Request.MinPrice)
            .GreaterThanOrEqualTo(0)
            .When(q => q.Request.MinPrice.HasValue)
            .WithMessage("Minimum price must be greater than or equal to 0");

        RuleFor(q => q.Request.MaxPrice)
            .GreaterThanOrEqualTo(0)
            .When(q => q.Request.MaxPrice.HasValue)
            .WithMessage("Maximum price must be greater than or equal to 0");

        RuleFor(q => q.Request.MaxPrice)
            .GreaterThanOrEqualTo(q => q.Request.MinPrice)
            .When(q => q.Request.MinPrice.HasValue && q.Request.MaxPrice.HasValue)
            .WithMessage("Maximum price must be greater than or equal to minimum price");

        RuleFor(q => q.Request.SortBy)
            .IsInEnum()
            .WithMessage("Invalid sort option");

        RuleFor(q => q.Request.PageRequest)
            .NotNull()
            .WithMessage("Page request is required");
    }
}

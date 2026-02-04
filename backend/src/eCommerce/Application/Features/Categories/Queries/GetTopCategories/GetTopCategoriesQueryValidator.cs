using FluentValidation;

namespace Application.Features.Categories.Queries.GetTopCategories;

public class GetTopCategoriesQueryValidator : AbstractValidator<GetTopCategoriesQuery>
{
    public GetTopCategoriesQueryValidator() { }
}
using FluentValidation;

namespace Application.Features.Comments.Commands.Create;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.ProductId).NotEmpty();
        RuleFor(c => c.CommentText).NotEmpty();
        RuleFor(c => c.CommentDate).NotEmpty();
    }
}
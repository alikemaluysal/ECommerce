using FluentValidation;

namespace Application.Features.Comments.Commands.Update;

public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.ProductId).NotEmpty();
        RuleFor(c => c.CommentText).NotEmpty();
        RuleFor(c => c.CommentDate).NotEmpty();
    }
}
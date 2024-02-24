using NArchitecture.Core.Application.Responses;

namespace Application.Features.Comments.Commands.Update;

public class UpdatedCommentResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public string CommentText { get; set; }
    public DateTime CommentDate { get; set; }
}
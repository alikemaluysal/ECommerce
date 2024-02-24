using NArchitecture.Core.Application.Responses;

namespace Application.Features.Comments.Queries.GetById;

public class GetByIdCommentResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public string CommentText { get; set; }
    public DateTime CommentDate { get; set; }
}
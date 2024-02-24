using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Comments.Queries.GetList;

public class GetListCommentListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public string CommentText { get; set; }
    public DateTime CommentDate { get; set; }
}
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Comments.Commands.Delete;

public class DeletedCommentResponse : IResponse
{
    public Guid Id { get; set; }
}
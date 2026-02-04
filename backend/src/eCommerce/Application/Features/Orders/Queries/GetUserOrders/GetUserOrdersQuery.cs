using Application.Features.Orders.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.Orders.Constants.OrdersOperationClaims;

namespace Application.Features.Orders.Queries.GetUserOrders;

public class GetUserOrdersQuery : IRequest<GetListResponse<GetUserOrdersListItemDto>>, ISecuredRequest
{
    public Guid UserId { get; set; }
    public PageRequest PageRequest { get; set; } = new() { PageIndex = 0, PageSize = 10 };

    public string[] Roles => [Admin, Read];

    public class GetUserOrdersQueryHandler : IRequestHandler<GetUserOrdersQuery, GetListResponse<GetUserOrdersListItemDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetUserOrdersQueryHandler(
            IOrderRepository orderRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetUserOrdersListItemDto>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Order> orders = await _orderRepository.GetListAsync(
                predicate: o => o.UserId == request.UserId,
                include: i => i.Include(o => o.OrderItems).ThenInclude(oi => oi.Product),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                orderBy: q => q.OrderByDescending(o => o.CreatedDate)
            );

            GetListResponse<GetUserOrdersListItemDto> response = _mapper.Map<GetListResponse<GetUserOrdersListItemDto>>(orders);
            return response;
        }
    }
}

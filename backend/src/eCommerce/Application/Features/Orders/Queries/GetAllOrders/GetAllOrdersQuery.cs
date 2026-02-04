using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.ApplicationOperationClaims;

namespace Application.Features.Orders.Queries.GetAllOrders;

public class GetAllOrdersQuery : IRequest<GetListResponse<GetAllOrdersListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; } = new() { PageIndex = 0, PageSize = 10 };

    public string[] Roles => [Admin];

    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, GetListResponse<GetAllOrdersListItemDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetAllOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetAllOrdersListItemDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Order> orders = await _orderRepository.GetListAsync(
                include: i => i.Include(o => o.User).Include(o => o.OrderItems).ThenInclude(oi => oi.Product),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                orderBy: q => q.OrderByDescending(o => o.CreatedDate)
            );

            GetListResponse<GetAllOrdersListItemDto> response = _mapper.Map<GetListResponse<GetAllOrdersListItemDto>>(orders);
            return response;
        }
    }
}

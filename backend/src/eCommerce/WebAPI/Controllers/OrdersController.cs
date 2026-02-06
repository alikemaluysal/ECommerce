using Application.Features.Orders.Commands.Create;
using Application.Features.Orders.Commands.Delete;
using Application.Features.Orders.Commands.Update;
using Application.Features.Orders.Commands.Checkout;
using Application.Features.Orders.Commands.UpdateOrderStatus;
using Application.Features.Orders.Queries.GetById;
using Application.Features.Orders.Queries.GetList;
using Application.Features.Orders.Queries.GetUserOrders;
using Application.Features.Orders.Queries.GetAllOrders;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : BaseController
{
    [HttpPost("checkout")]
    public async Task<ActionResult<CheckedOutResponse>> Checkout([FromBody] CheckoutCommand command)
    {
        command.UserId = getUserIdFromRequest();
        CheckedOutResponse response = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut("{id}/status")]
    public async Task<ActionResult<UpdatedOrderStatusResponse>> UpdateStatus(
        [FromRoute] Guid id,
        [FromBody] UpdateOrderStatusRequest request)
    {
        Guid userId = getUserIdFromRequest();
        var command = new UpdateOrderStatusCommand(id, request)
        {
            UserId = userId,
            IsAdminRequest = false
        };
        UpdatedOrderStatusResponse response = await Mediator.Send(command);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetUserOrdersListItemDto>>> GetUserOrders([FromQuery] PageRequest pageRequest)
    {
        Guid userId = getUserIdFromRequest();
        GetUserOrdersQuery query = new() { UserId = userId, PageRequest = pageRequest };
        GetListResponse<GetUserOrdersListItemDto> response = await Mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdOrderResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdOrderQuery query = new() { Id = id };
        GetByIdOrderResponse response = await Mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("admin")]
    public async Task<ActionResult<GetListResponse<GetAllOrdersListItemDto>>> GetAllOrders([FromQuery] PageRequest pageRequest)
    {
        GetAllOrdersQuery query = new() { PageRequest = pageRequest };
        GetListResponse<GetAllOrdersListItemDto> response = await Mediator.Send(query);
        return Ok(response);
    }

    [HttpPut("{id}/admin/status")]
    public async Task<ActionResult<UpdatedOrderStatusResponse>> AdminUpdateOrderStatus(
        [FromRoute] Guid id,
        [FromBody] UpdateOrderStatusRequest request)
    {
        var command = new UpdateOrderStatusCommand(id, request)
        {
            IsAdminRequest = true
        };
        UpdatedOrderStatusResponse response = await Mediator.Send(command);
        return Ok(response);
    }
}
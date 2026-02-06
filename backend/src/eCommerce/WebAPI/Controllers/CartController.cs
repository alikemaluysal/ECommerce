using Application.Features.Cart.Commands.AddToCart;
using Application.Features.Cart.Commands.UpdateCartItem;
using Application.Features.Cart.Commands.RemoveFromCart;
using Application.Features.Cart.Commands.ClearCart;
using Application.Features.Cart.Queries.GetCart;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : BaseController
{
    [HttpPost("add")]
    public async Task<ActionResult<AddedToCartResponse>> AddToCart([FromBody] AddToCartCommand command)
    {
        command.UserId = getUserIdFromRequest();
        AddedToCartResponse response = await Mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("update/{itemId}")]
    public async Task<ActionResult<UpdatedCartItemResponse>> UpdateCartItem(
        [FromRoute] Guid itemId,
        [FromBody] UpdateCartItemRequest request)
    {
        Guid userId = getUserIdFromRequest();
        var command = new UpdateCartItemCommand(itemId, userId, request);
        UpdatedCartItemResponse response = await Mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("remove/{itemId}")]
    public async Task<ActionResult<RemovedFromCartResponse>> RemoveFromCart([FromRoute] Guid itemId)
    {
        Guid userId = getUserIdFromRequest();
        RemoveFromCartCommand command = new() { ItemId = itemId, UserId = userId };
        RemovedFromCartResponse response = await Mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("clear")]
    public async Task<ActionResult<ClearedCartResponse>> ClearCart()
    {
        Guid userId = getUserIdFromRequest();
        ClearCartCommand command = new() { UserId = userId };
        ClearedCartResponse response = await Mediator.Send(command);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetCartResponse>> GetCart()
    {
        Guid userId = getUserIdFromRequest();
        GetCartQuery query = new() { UserId = userId };
        GetCartResponse response = await Mediator.Send(query);
        return Ok(response);
    }
}

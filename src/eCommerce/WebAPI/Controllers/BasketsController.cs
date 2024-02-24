using Application.Features.Baskets.Commands.Create;
using Application.Features.Baskets.Commands.Delete;
using Application.Features.Baskets.Commands.Update;
using Application.Features.Baskets.Queries.GetById;
using Application.Features.Baskets.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateBasketCommand createBasketCommand)
    {
        CreatedBasketResponse response = await Mediator.Send(createBasketCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateBasketCommand updateBasketCommand)
    {
        UpdatedBasketResponse response = await Mediator.Send(updateBasketCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedBasketResponse response = await Mediator.Send(new DeleteBasketCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdBasketResponse response = await Mediator.Send(new GetByIdBasketQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListBasketQuery getListBasketQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListBasketListItemDto> response = await Mediator.Send(getListBasketQuery);
        return Ok(response);
    }
}
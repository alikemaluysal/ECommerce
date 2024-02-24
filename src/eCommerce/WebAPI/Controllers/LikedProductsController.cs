using Application.Features.LikedProducts.Commands.Create;
using Application.Features.LikedProducts.Commands.Delete;
using Application.Features.LikedProducts.Commands.Update;
using Application.Features.LikedProducts.Queries.GetById;
using Application.Features.LikedProducts.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LikedProductsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateLikedProductCommand createLikedProductCommand)
    {
        CreatedLikedProductResponse response = await Mediator.Send(createLikedProductCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateLikedProductCommand updateLikedProductCommand)
    {
        UpdatedLikedProductResponse response = await Mediator.Send(updateLikedProductCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedLikedProductResponse response = await Mediator.Send(new DeleteLikedProductCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdLikedProductResponse response = await Mediator.Send(new GetByIdLikedProductQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListLikedProductQuery getListLikedProductQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListLikedProductListItemDto> response = await Mediator.Send(getListLikedProductQuery);
        return Ok(response);
    }
}
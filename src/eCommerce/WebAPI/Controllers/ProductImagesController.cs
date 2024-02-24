using Application.Features.ProductImages.Commands.Create;
using Application.Features.ProductImages.Commands.Delete;
using Application.Features.ProductImages.Commands.Update;
using Application.Features.ProductImages.Queries.GetById;
using Application.Features.ProductImages.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductImagesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateProductImageCommand createProductImageCommand)
    {
        CreatedProductImageResponse response = await Mediator.Send(createProductImageCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateProductImageCommand updateProductImageCommand)
    {
        UpdatedProductImageResponse response = await Mediator.Send(updateProductImageCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedProductImageResponse response = await Mediator.Send(new DeleteProductImageCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdProductImageResponse response = await Mediator.Send(new GetByIdProductImageQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListProductImageQuery getListProductImageQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListProductImageListItemDto> response = await Mediator.Send(getListProductImageQuery);
        return Ok(response);
    }
}
using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Commands.AddProductImage;
using Application.Features.Products.Commands.DeleteProductImage;
using Application.Features.Products.Commands.SetPrimaryImage;
using Application.Features.Products.Commands.AddProductSpecification;
using Application.Features.Products.Commands.UpdateProductSpecification;
using Application.Features.Products.Commands.DeleteProductSpecification;
using Application.Features.Products.Queries.GetById;
using Application.Features.Products.Queries.GetList;
using Application.Features.Products.Queries.GetProductsByCategory;
using Application.Features.Products.Queries.GetProductSpecifications;
using Application.Features.Products.Queries.Search;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedProductResponse>> Add([FromBody] CreateProductCommand command)
    {
        CreatedProductResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdatedProductResponse>> Update([FromRoute] Guid id, [FromBody] UpdateProductRequest request)
    {
        var command = new UpdateProductCommand(id, request);
        UpdatedProductResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedProductResponse>> Delete([FromRoute] Guid id)
    {
        DeleteProductCommand command = new() { Id = id };

        DeletedProductResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdProductResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdProductQuery query = new() { Id = id };

        GetByIdProductResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListProductListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListProductQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListProductListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet("category/{categoryId}")]
    public async Task<ActionResult<List<GetProductsByCategoryResponse>>> GetProductsByCategory([FromRoute] Guid categoryId)
    {
        GetProductsByCategoryQuery query = new() { CategoryId = categoryId };

        List<GetProductsByCategoryResponse> response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet("search")]
    public async Task<ActionResult<GetListResponse<SearchProductsListItemDto>>> SearchProducts(
        [FromQuery] SearchProductsRequest request)
    {
        var query = new SearchProductsQuery(request);
        GetListResponse<SearchProductsListItemDto> response = await Mediator.Send(query);
        return Ok(response);
    }


    [HttpPost("{id}/images")]
    public async Task<ActionResult<AddedProductImageResponse>> AddProductImage([FromRoute] Guid id, [FromForm] AddProductImageRequest request)
    {
        var command = new AddProductImageCommand(id, request);
        AddedProductImageResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}/images/{imageId}")]
    public async Task<ActionResult<DeletedProductImageResponse>> DeleteProductImage([FromRoute] Guid id, [FromRoute] Guid imageId)
    {
        DeleteProductImageCommand command = new() { ProductId = id, ImageId = imageId };

        DeletedProductImageResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpPut("{id}/images/{imageId}/set-primary")]
    public async Task<ActionResult<SetPrimaryImageResponse>> SetPrimaryImage([FromRoute] Guid id, [FromRoute] Guid imageId)
    {
        SetPrimaryImageCommand command = new() { ProductId = id, ImageId = imageId };

        SetPrimaryImageResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpPost("{id}/specifications")]
    public async Task<ActionResult<AddedProductSpecificationResponse>> AddProductSpecification([FromRoute] Guid id, [FromBody] AddProductSpecificationRequest request)
    {
        var command = new AddProductSpecificationCommand(id, request);
        AddedProductSpecificationResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpPut("{id}/specifications/{specId}")]
    public async Task<ActionResult<UpdatedProductSpecificationResponse>> UpdateProductSpecification(
        [FromRoute] Guid id,
        [FromRoute] Guid specId,
        [FromBody] UpdateProductSpecificationRequest request)
    {
        var command = new UpdateProductSpecificationCommand(id, specId, request);
        UpdatedProductSpecificationResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}/specifications/{specId}")]
    public async Task<ActionResult<DeletedProductSpecificationResponse>> DeleteProductSpecification([FromRoute] Guid id, [FromRoute] Guid specId)
    {
        DeleteProductSpecificationCommand command = new() { ProductId = id, SpecId = specId };

        DeletedProductSpecificationResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}/specifications")]
    public async Task<ActionResult<List<GetProductSpecificationsResponse>>> GetProductSpecifications([FromRoute] Guid id)
    {
        GetProductSpecificationsQuery query = new() { ProductId = id };

        List<GetProductSpecificationsResponse> response = await Mediator.Send(query);

        return Ok(response);
    }
}
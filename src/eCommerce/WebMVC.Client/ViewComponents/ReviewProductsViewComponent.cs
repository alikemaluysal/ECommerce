using WebMVC.Client.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Client.HttpClientHelpers.Responses;
using WebMVC.Client.Services;

namespace WebMVC.Client.ViewComponents;

public class ReviewProductsViewComponent : ViewComponent
{

    private readonly IApiService _apiService;

    public ReviewProductsViewComponent(IApiService apiService)
    {
        _apiService = apiService;
    }


    public async Task<IViewComponentResult> InvokeAsync()
    {
        int pageIndex = 0, pageSize = 9;
        var query = new Dictionary<string, string>();

        query["PageRequest.PageIndex"] = pageIndex.ToString();
        query["PageRequest.PageSize"] = pageSize.ToString();

        var latestProducts = await _apiService.GetAsync<PagedRequestResponse<ProductListingViewModel>>("Products/GetProductDetails", query);


        var viewModel = new OwlCarouselViewModel
        {
            Title = "Review Products",
            Items = latestProducts.Items
        };
        return View(viewModel);

    }

}
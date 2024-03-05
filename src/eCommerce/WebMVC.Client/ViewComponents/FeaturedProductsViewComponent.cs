using Microsoft.AspNetCore.Mvc;
using WebMVC.Client.HttpClientHelpers.Responses;
using WebMVC.Client.Models.ViewModels;
using WebMVC.Client.Services;


namespace WebMVC.Client.ViewComponents;
public class FeaturedProductsViewComponent : ViewComponent
{
    private readonly IApiService _apiService;

    public FeaturedProductsViewComponent(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        int pageIndex = 0, pageSize = 10;
        var query = new Dictionary<string, string>();

        query["PageRequest.PageIndex"] = pageIndex.ToString();
        query["PageRequest.PageSize"] = pageSize.ToString();

        var featuredProducts = await _apiService.GetAsync<PagedRequestResponse<FeaturedProductViewModel>>("Products/GetProductDetails", query);

        return View(featuredProducts.Items);
    }

}

using WebMVC.Client.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Client.HttpClientHelpers.Responses;
using WebMVC.Client.Services;
namespace WebMVC.Client.ViewComponents;

public class CategoryListViewComponent : ViewComponent
{
    private readonly IApiService _apiService;

    public CategoryListViewComponent(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        int pageIndex = 0, pageSize = 10;
        var query = new Dictionary<string, string>();

        query["PageRequest.PageIndex"] = pageIndex.ToString();
        query["PageRequest.PageSize"] = pageSize.ToString();

        var categoriesList = await _apiService.GetAsync<PagedRequestResponse<CategoryListViewModel>>("Categories", query);

        return View(categoriesList.Items);
    }
 
}

using WebMVC.Client.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Client.Services;
using WebMVC.Client.HttpClientHelpers.Responses;


namespace WebMVC.Client.Controllers;
public class HomeController : Controller
{
    private readonly IApiService _apiService;

    public HomeController(IApiService apiService)
    {
        _apiService = apiService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [Route("/about-us")]
    [HttpGet]
    public IActionResult AboutUs()
    {
        return View();
    }

    [Route("/contact")]
    [HttpGet]
    public IActionResult Contact()
    {
        return View();
    }

    [Route("/contact")]
    [HttpPost]
    public async Task<IActionResult> Contact([FromForm] NewContactFormMessageViewModel newContactMessage)
    {
        if (!ModelState.IsValid)
        {
            return View(newContactMessage);
        }

        return View();
    }

    [Route("/product/list")]
    [HttpGet]
    public async Task<IActionResult> Listing(int pageIndex = 0, int pageSize = 6)
    {

        var query = new Dictionary<string, string>();

        query["PageRequest.PageIndex"] = pageIndex.ToString();
        query["PageRequest.PageSize"] = pageSize.ToString();

        var products = await _apiService.GetAsync<PagedRequestResponse<ProductListingViewModel>>("Products/GetProductDetails", query);

        return View(products);

    }

    [Route("/product/{productId:int}/details")]
    [HttpGet]
    public async Task<IActionResult> ProductDetail([FromRoute] int productId)
    {

        return View();
    }
}

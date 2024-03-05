using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Client.ViewComponents;
public class TopRatedProductsViewComponent : ViewComponent
{

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }

}

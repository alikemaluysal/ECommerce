using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Client.ViewComponents;
public class LatestProductsViewComponent : ViewComponent
{

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();

    }

}

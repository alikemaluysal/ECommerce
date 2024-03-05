using Microsoft.AspNetCore.Mvc;


namespace WebMVC.Client.ViewComponents;
public class FeaturedProductsViewComponent : ViewComponent
{

    public async Task<IViewComponentResult> InvokeAsync()
    {

        return View();
    }

}

using WebMVC.Client.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Client.ViewComponents;
public class ReviewProductsViewComponent : ViewComponent
{

    public async Task<IViewComponentResult> InvokeAsync()
    {

        return View();

    }

}

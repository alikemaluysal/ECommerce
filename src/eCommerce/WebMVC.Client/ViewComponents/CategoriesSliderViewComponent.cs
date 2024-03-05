using WebMVC.Client.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Client.ViewComponents;

public class CategoriesSliderViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {

        return View(new List<CategorySliderViewModel>());
    }
}

using WebMVC.Client.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
namespace WebMVC.Client.ViewComponents;

public class CategoryListViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {

        return View();
    }

}

using WebMVC.Client.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace WebMVC.Client.Controllers
{
    public class HomeController : Controller
    {

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
        public async Task<IActionResult> Listing()
        {

            return View();

        }

        [Route("/product/{productId:int}/details")]
        [HttpGet]
        public async Task<IActionResult> ProductDetail([FromRoute] int productId)
        {

            return View();
        }
    }
}
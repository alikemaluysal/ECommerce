using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Client.Controllers
{
    public class CartController : Controller
    {
        [Route("/add-to-cart/{productId:int}")]
        [HttpGet]
        public IActionResult AddProduct([FromRoute] int productId)
        {
            var prevUrl = Request.Headers.Referer.FirstOrDefault();

            if (prevUrl is null)
            {
                return RedirectToAction(nameof(Edit));
            }

            return Redirect(prevUrl);
        }

        [Route("/cart")]
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [Route("/cart")]
        [HttpPost]
        public IActionResult Edit([FromForm] object editCartModel)
        {
            return View();
        }
    }
}
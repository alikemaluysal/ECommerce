﻿using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Client.Controllers
{
    public class ProfileController : Controller
    {
        [Route("/profile")]
        [HttpGet]
        public IActionResult Details()
        {
            return View();
        }

        [Route("/profile")]
        [HttpPost]
        public IActionResult Edit([FromForm] object editMyProfileModel)
        {
            return RedirectToAction(nameof(Details));
        }

        [Route("/my-orders")]
        [HttpGet]
        public IActionResult MyOrders()
        {
            return View();
        }

        [Route("/my-products")]
        [HttpGet]
        public IActionResult MyProducts()
        {
            return View();
        }
    }
}
﻿using Microsoft.AspNetCore.Mvc;

namespace Twitter.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

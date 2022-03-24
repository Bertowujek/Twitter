using Microsoft.AspNetCore.Mvc;

namespace Twitter.Controllers
{
    public class TweetsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

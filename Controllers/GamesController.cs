using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    public class GamesController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}

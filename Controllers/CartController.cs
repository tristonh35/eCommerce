using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly VideoGameContext _context;

        public CartController(VideoGameContext context)
        {
            _context = context;
        }

        public IActionResult Add(int id)
        {
            Game? gameToAdd = _context.Games.Where(g => g.GameId == id).SingleOrDefault();

            if(gameToAdd == null)
            {
                //Game with specified id does not exist
                TempData["Message"] = "Sorry, that game no longer exists";
                return RedirectToAction("Index", "Games");
            }
            TempData["Message"] = "Item added to the cart";
            return RedirectToAction("Index", "Games");
        }
    }
}

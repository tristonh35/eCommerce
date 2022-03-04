using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    public class GamesController : Controller
    {
        // Set _context as readonly so it cannot be assigned elsewhere other than
        // a constructor
        private readonly VideoGameContext _context;


        public GamesController(VideoGameContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Game g)
        {
            // If everything is valid in the game class
            // add and save changes
            if(ModelState.IsValid)
            {
                _context.Games.Add(g); //Prepares insert
                await _context.SaveChangesAsync(); //Execute pending insert
                ViewData["Message"] = $"{g.Title} was added successfully!";

                return View();
            }
            return View(g);
        }
    }
}

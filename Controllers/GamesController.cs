using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Index()
        {
            // Get all games from the database
            List<Game> games = await (from game in _context.Games select game).ToListAsync();
            // Show them on the web page
            return View(games);
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
        
        public async Task<IActionResult> Edit(int id)
        {
            Game? gameToEdit = await _context.Games.FindAsync(id);

            if (gameToEdit == null)
            {
                return NotFound();
            }
            return View(gameToEdit);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Game gameModel)
        {
            if(ModelState.IsValid)
            {
                _context.Games.Update(gameModel);
                await _context.SaveChangesAsync();

                TempData["Message"] = $"{gameModel.Title} was updated successfully!!";
                return RedirectToAction("Index");
                
            }
            return View(gameModel);
        }
        
    }
}

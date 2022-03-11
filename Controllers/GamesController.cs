﻿using eCommerce.Data;
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
        public async Task<IActionResult> Index(int? id)
        {
            const int NumGamesToDisplayPerPage = 3;
            const int PageOffset = 1; // Need a page offset to use current page and figure out, num games to skip
            // Set current page to id if it has a value, otherwise use 1.
            int currPage = id ?? 1; // Set currPage to id if it has a valuem otherwise use 1
            List<Game> games = await (from game in _context.Games 
                                     select game)
                                     .Skip(NumGamesToDisplayPerPage * (currPage - PageOffset))
                                     .Take(NumGamesToDisplayPerPage).ToListAsync();
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
        
        public async Task<IActionResult> Delete(int id)
        {
            Game? gameToDelete = await _context.Games.FindAsync(id);

            if(gameToDelete == null)
            {
                return NotFound();
            }
            return View(gameToDelete);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Game? gameToDelete = await _context.Games.FindAsync(id);

            if(gameToDelete != null)
            {
                _context.Games.Remove(gameToDelete);
                await _context.SaveChangesAsync();
                TempData["Message"] = gameToDelete.Title + " was deleted successfully";
                return RedirectToAction("Index");
            }

            TempData["Message"] = "This game was already deleted";
            return RedirectToAction("Index");
          
        }

        public async Task<IActionResult> Details(int id)
        {
            Game? gameDetails = await _context.Games.FindAsync(id);

            if (gameDetails == null)
            {
                return NotFound();
            }
            return View(gameDetails);
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.EF;
using Domain;

namespace WebApp.Controllers
{
    public class RecipeIngredientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecipeIngredientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RecipeIngredients
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RecipeIngredients.Include(r => r.Ingredient).Include(r => r.Recipe);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RecipeIngredients/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.RecipeIngredients == null)
            {
                return NotFound();
            }

            var recipeIngredient = await _context.RecipeIngredients
                .Include(r => r.Ingredient)
                .Include(r => r.Recipe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipeIngredient == null)
            {
                return NotFound();
            }

            return View(recipeIngredient);
        }

        // GET: RecipeIngredients/Create
        public IActionResult Create()
        {
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name");
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Description");
            return View();
        }

        // POST: RecipeIngredients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecipeId,IngredientId,Quantity,Id")] RecipeIngredient recipeIngredient)
        {
            if (ModelState.IsValid)
            {
                recipeIngredient.Id = Guid.NewGuid();
                _context.Add(recipeIngredient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name", recipeIngredient.IngredientId);
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Description", recipeIngredient.RecipeId);
            return View(recipeIngredient);
        }

        // GET: RecipeIngredients/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.RecipeIngredients == null)
            {
                return NotFound();
            }

            var recipeIngredient = await _context.RecipeIngredients.FindAsync(id);
            if (recipeIngredient == null)
            {
                return NotFound();
            }
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name", recipeIngredient.IngredientId);
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Description", recipeIngredient.RecipeId);
            return View(recipeIngredient);
        }

        // POST: RecipeIngredients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RecipeId,IngredientId,Quantity,Id")] RecipeIngredient recipeIngredient)
        {
            if (id != recipeIngredient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipeIngredient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeIngredientExists(recipeIngredient.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name", recipeIngredient.IngredientId);
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Description", recipeIngredient.RecipeId);
            return View(recipeIngredient);
        }

        // GET: RecipeIngredients/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.RecipeIngredients == null)
            {
                return NotFound();
            }

            var recipeIngredient = await _context.RecipeIngredients
                .Include(r => r.Ingredient)
                .Include(r => r.Recipe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipeIngredient == null)
            {
                return NotFound();
            }

            return View(recipeIngredient);
        }

        // POST: RecipeIngredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.RecipeIngredients == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RecipeIngredients'  is null.");
            }
            var recipeIngredient = await _context.RecipeIngredients.FindAsync(id);
            if (recipeIngredient != null)
            {
                _context.RecipeIngredients.Remove(recipeIngredient);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeIngredientExists(Guid id)
        {
          return (_context.RecipeIngredients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

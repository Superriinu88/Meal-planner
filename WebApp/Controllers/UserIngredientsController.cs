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
    public class UserIngredientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserIngredientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserIngredients
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserIngredients.Include(u => u.AppUser).Include(u => u.Ingredient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserIngredients/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.UserIngredients == null)
            {
                return NotFound();
            }

            var userIngredient = await _context.UserIngredients
                .Include(u => u.AppUser)
                .Include(u => u.Ingredient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userIngredient == null)
            {
                return NotFound();
            }

            return View(userIngredient);
        }

        // GET: UserIngredients/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name");
            return View();
        }

        // POST: UserIngredients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserId,IngredientId,Quantity,Id")] UserIngredient userIngredient)
        {
            if (ModelState.IsValid)
            {
                userIngredient.Id = Guid.NewGuid();
                _context.Add(userIngredient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userIngredient.AppUserId);
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name", userIngredient.IngredientId);
            return View(userIngredient);
        }

        // GET: UserIngredients/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.UserIngredients == null)
            {
                return NotFound();
            }

            var userIngredient = await _context.UserIngredients.FindAsync(id);
            if (userIngredient == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userIngredient.AppUserId);
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name", userIngredient.IngredientId);
            return View(userIngredient);
        }

        // POST: UserIngredients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AppUserId,IngredientId,Quantity,Id")] UserIngredient userIngredient)
        {
            if (id != userIngredient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userIngredient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserIngredientExists(userIngredient.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userIngredient.AppUserId);
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name", userIngredient.IngredientId);
            return View(userIngredient);
        }

        // GET: UserIngredients/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.UserIngredients == null)
            {
                return NotFound();
            }

            var userIngredient = await _context.UserIngredients
                .Include(u => u.AppUser)
                .Include(u => u.Ingredient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userIngredient == null)
            {
                return NotFound();
            }

            return View(userIngredient);
        }

        // POST: UserIngredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.UserIngredients == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UserIngredients'  is null.");
            }
            var userIngredient = await _context.UserIngredients.FindAsync(id);
            if (userIngredient != null)
            {
                _context.UserIngredients.Remove(userIngredient);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserIngredientExists(Guid id)
        {
          return (_context.UserIngredients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

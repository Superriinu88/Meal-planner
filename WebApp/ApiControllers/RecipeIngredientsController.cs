using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.EF;
using Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeIngredientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RecipeIngredientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RecipeIngredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeIngredient>>> GetRecipeIngredients()
        {
          if (_context.RecipeIngredients == null)
          {
              return NotFound();
          }
            return await _context.RecipeIngredients.ToListAsync();
        }

        // GET: api/RecipeIngredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeIngredient>> GetRecipeIngredient(Guid id)
        {
          if (_context.RecipeIngredients == null)
          {
              return NotFound();
          }
            var recipeIngredient = await _context.RecipeIngredients.FindAsync(id);

            if (recipeIngredient == null)
            {
                return NotFound();
            }

            return recipeIngredient;
        }

        // PUT: api/RecipeIngredients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipeIngredient(Guid id, RecipeIngredient recipeIngredient)
        {
            if (id != recipeIngredient.Id)
            {
                return BadRequest();
            }

            _context.Entry(recipeIngredient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeIngredientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RecipeIngredients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RecipeIngredient>> PostRecipeIngredient(RecipeIngredient recipeIngredient)
        {
          if (_context.RecipeIngredients == null)
          {
              return Problem("Entity set 'ApplicationDbContext.RecipeIngredients'  is null.");
          }
            _context.RecipeIngredients.Add(recipeIngredient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipeIngredient", new { id = recipeIngredient.Id }, recipeIngredient);
        }

        // DELETE: api/RecipeIngredients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipeIngredient(Guid id)
        {
            if (_context.RecipeIngredients == null)
            {
                return NotFound();
            }
            var recipeIngredient = await _context.RecipeIngredients.FindAsync(id);
            if (recipeIngredient == null)
            {
                return NotFound();
            }

            _context.RecipeIngredients.Remove(recipeIngredient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeIngredientExists(Guid id)
        {
            return (_context.RecipeIngredients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

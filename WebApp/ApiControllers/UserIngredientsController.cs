
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.EF;
using Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserIngredientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserIngredientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserIngredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserIngredient>>> GetUserIngredients()
        {
          if (_context.UserIngredients == null)
          {
              return NotFound();
          }
            return await _context.UserIngredients.ToListAsync();
        }

        // GET: api/UserIngredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserIngredient>> GetUserIngredient(Guid id)
        {
          if (_context.UserIngredients == null)
          {
              return NotFound();
          }
            var userIngredient = await _context.UserIngredients.FindAsync(id);

            if (userIngredient == null)
            {
                return NotFound();
            }

            return userIngredient;
        }

        // PUT: api/UserIngredients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserIngredient(Guid id, UserIngredient userIngredient)
        {
            if (id != userIngredient.Id)
            {
                return BadRequest();
            }

            _context.Entry(userIngredient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserIngredientExists(id))
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

        // POST: api/UserIngredients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserIngredient>> PostUserIngredient(UserIngredient userIngredient)
        {
          if (_context.UserIngredients == null)
          {
              return Problem("Entity set 'ApplicationDbContext.UserIngredients'  is null.");
          }
            _context.UserIngredients.Add(userIngredient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserIngredient", new { id = userIngredient.Id }, userIngredient);
        }

        // DELETE: api/UserIngredients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserIngredient(Guid id)
        {
            if (_context.UserIngredients == null)
            {
                return NotFound();
            }
            var userIngredient = await _context.UserIngredients.FindAsync(id);
            if (userIngredient == null)
            {
                return NotFound();
            }

            _context.UserIngredients.Remove(userIngredient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserIngredientExists(Guid id)
        {
            return (_context.UserIngredients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

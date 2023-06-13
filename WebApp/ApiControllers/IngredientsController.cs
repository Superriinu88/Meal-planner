
using DAL.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.EF;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Public.DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IAppUow _uow;

        public IngredientsController(IAppUow uow)
        {
            _uow = uow;
        }

        // GET: api/Ingredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientDto>>> GetIngredients()
        {
          
            var res =  await _uow.IngredientRepository.AllIngredientsAsync();
            return Ok(res);
        }

        // GET: api/Ingredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientDto>> GetIngredient(Guid id)
        {
          
            var ingredient = await _uow.IngredientRepository.FindIngredientAsync(id)!;
            

         
            return ingredient;
        }

        // PUT: api/Ingredients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredient(Guid id, Ingredient ingredient)
        {
            if (id != ingredient.Id)
            {
                return BadRequest();
            }

            _uow.IngredientRepository.Update(ingredient);

            return NoContent();
        }

        // POST: api/Ingredients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Ingredient>> PostIngredient(Ingredient ingredient)
        {
         
            _uow.IngredientRepository.Add(ingredient);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetIngredient", new { id = ingredient.Id }, ingredient);
        }

        // DELETE: api/Ingredients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(Guid id)
        {
          
            var ingredient = await _uow.IngredientRepository.FindAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            _uow.IngredientRepository.Remove(ingredient);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

    
    }
}

using System.Net;
using DAL.Contracts;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Helpers.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Public.DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IAppUow _uow;


        public RecipesController(IAppUow uow)
        {
            _uow = uow;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeGetAllDto>>> GetRecipes()
        {
            var vm = await _uow.RecipeRepository.AllRecipesAsync();
            return Ok(vm);
        }
        
        // GET: api/Recipes
        [HttpGet("findRecipes")]
        public async Task<ActionResult<IEnumerable<RecipeGetAllDto>>> GetRecipesBySearchAsync(string ingredient)
        {
            
            
            var vm = await _uow.RecipeRepository.AllRecipesWithUserSearchAsync(ingredient);
            return Ok(vm);
        }


        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeGetOneDto>> GetRecipe(Guid id)
        {
            var recipe = await _uow.RecipeRepository.FindRecipeAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return recipe;
        }

        // PUT: api/Recipes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(Guid id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return BadRequest();
            }

            _uow.RecipeRepository.Update(recipe);


            return NoContent();
        }

        // POST: api/Recipes
        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Recipe>> PostRecipe(RecipeDto recipe)
        {
            if (!recipe.IngredientsList.Any())
            {
                return BadRequest(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "You have add at least one ingredient"
                });
            }
            
            recipe.AppUserId = User.GetUserId();
            var newRecipe = _uow.RecipeRepository.AddRecipe(recipe);
            if (newRecipe != null)
                _uow.RecipeIngredientsRepository.AddRecipeIngredient(newRecipe.Id, recipe.IngredientsList);


            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetRecipe", new {id = recipe.Id}, recipe);
        }

        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(Guid id)
        {
            var recipe = await _uow.RecipeRepository.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _uow.RecipeRepository.Remove(recipe);
            await _uow.SaveChangesAsync();

            return NoContent();
        }
    }
}

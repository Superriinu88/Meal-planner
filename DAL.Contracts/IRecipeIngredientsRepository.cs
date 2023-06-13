using DAL.Contracts.Base;
using Domain;
using Public.DTO;

namespace DAL.Contracts;

public interface IRecipeIngredientsRepository : IBaseRepository<RecipeIngredient>
{
    
    void AddRecipeIngredient(Guid recipeId, IEnumerable<IngredientDto> IngredientsList);
}

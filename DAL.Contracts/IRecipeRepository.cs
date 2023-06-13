using DAL.Contracts.Base;
using Domain;
using Public.DTO;

namespace DAL.Contracts;

public interface IRecipeRepository: IBaseRepository<Recipe>
{
    RecipeDto? AddRecipe(RecipeDto recipePost);
    Task<IEnumerable<RecipeGetAllDto>> AllRecipesAsync();
    Task<IEnumerable<RecipeGetAllDto>> AllRecipesWithUserSearchAsync(string ingredients);
    Task<RecipeGetOneDto> FindRecipeAsync(Guid id);
}

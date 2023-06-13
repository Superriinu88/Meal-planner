using DAL.Contracts;
using DAL.EF.Base;
using Domain;
using Public.DTO;

namespace DAL.EF.Repositories;

public class RecipeIngredientsRepository : EfBaseRepository<RecipeIngredient, ApplicationDbContext>,
    IRecipeIngredientsRepository
{
    public RecipeIngredientsRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public void AddRecipeIngredient(Guid recipeId, IEnumerable<IngredientDto> ingredientsList)
    {

        foreach (var ingredient in ingredientsList)
        {
            var newRecIng = RepositoryDbSet.Add(new RecipeIngredient()
            {
                IngredientId = ingredient.Id,
                RecipeId = recipeId,
                Quantity = ingredient.Quantity
  
            }).Entity;
        }
       
        
        
    }
}

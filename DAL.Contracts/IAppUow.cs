using DAL.Contracts.Base;

namespace DAL.Contracts;

public interface IAppUow:  IBaseUow
{
    IRecipeRepository RecipeRepository { get; }
    IIngredientRepository IngredientRepository { get; }
    IRecipeIngredientsRepository RecipeIngredientsRepository { get; }
    IUserIngredientsRepository UserIngredientsRepository { get; }
}

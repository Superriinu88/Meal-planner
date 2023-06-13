using DAL.Contracts;
using DAL.EF.Base;
using DAL.EF.Repositories;

namespace DAL.EF;

public class AppUow : EfBaseUow<ApplicationDbContext>, IAppUow
{
    public AppUow(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    private IRecipeRepository _recipeRepository;
    private IIngredientRepository _ingredientRepository;
    private IRecipeIngredientsRepository _recipeIngredientsRepository;
    private IUserIngredientsRepository _userIngredientsRepository;
    
    public IRecipeRepository RecipeRepository =>
        _recipeRepository ??= new RecipeRepository(UowDbContext);
    
    public IIngredientRepository IngredientRepository =>
        _ingredientRepository ??= new IngredientRepository(UowDbContext);
    
    public IRecipeIngredientsRepository RecipeIngredientsRepository =>
        _recipeIngredientsRepository ??= new RecipeIngredientsRepository(UowDbContext);
    public IUserIngredientsRepository UserIngredientsRepository =>
        _userIngredientsRepository ??= new UserIngredientsRepository(UowDbContext);


}

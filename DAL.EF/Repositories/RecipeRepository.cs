using DAL.Contracts;
using DAL.EF.Base;
using Domain;
using Microsoft.EntityFrameworkCore;
using Public.DTO;

namespace DAL.EF.Repositories;

public class RecipeRepository : EfBaseRepository<Recipe, ApplicationDbContext>, IRecipeRepository
{
    public RecipeRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public RecipeDto? AddRecipe(RecipeDto recipePost)
    {
        var newRecipe = RepositoryDbSet.Add(new Recipe()
        {
            Name = recipePost.Name,
            Instructions = recipePost.Instructions,
            Description = recipePost.Description,
            Servings = recipePost.Servings,
            PrepTime = recipePost.PrepTime,
            AppUserId = recipePost.AppUserId
        }).Entity;

        recipePost.Id = newRecipe.Id;
        return recipePost;
    }

    public async Task<IEnumerable<RecipeGetAllDto>> AllRecipesAsync()
    {
        return await RepositoryDbSet
            .AsNoTracking()
            .Select(recipe => new RecipeGetAllDto()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                PrepTime = recipe.PrepTime,
                Servings = recipe.Servings,
            }).OrderBy(e => e.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<RecipeGetAllDto>> AllRecipesWithUserSearchAsync(string ingredients)
    {
        var search = ingredients.Trim().ToUpper();

        return await RepositoryDbSet
            .Include(e => e.RecipeIngredients)!
            .ThenInclude(e => e.Ingredient)
            .AsNoTracking()
            .Where(r => r.Name.ToUpper().Contains(search) || r.Description.ToUpper().Contains(search) ||
                        r.Instructions.ToUpper().Contains(search) || r.RecipeIngredients!
                            .Any(i => i.Ingredient!.Name.ToUpper().Contains(search)))
            .Select(recipe => new RecipeGetAllDto()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                PrepTime = recipe.PrepTime,
                Servings = recipe.Servings,
            }).OrderBy(e => e.Name)
            .ToListAsync();
    }


    public async Task<RecipeGetOneDto> FindRecipeAsync(Guid id)
    {
        return (await RepositoryDbSet
            .Include(a => a.RecipeIngredients)
            .AsNoTracking()
            .Where(m => m.Id == id)
            .Select(recipe => new RecipeGetOneDto
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                Servings = recipe.Servings,
                Instructions = recipe.Instructions,
                PrepTime = recipe.Servings,
                IngredientsList = recipe.RecipeIngredients!.Where(ri => ri.RecipeId == id)
                    .Select(ri => ri.Ingredient)
                    .Select(i => new IngredientGetDto()
                    {
                        Name = i!.Name,
                        Quantity = i.RecipeIngredients!.Select(e => e.Quantity).First()
                    })
            })
            .FirstOrDefaultAsync())!;
    }
}

using DAL.Contracts;
using DAL.EF.Base;
using Domain;
using Microsoft.EntityFrameworkCore;
using Public.DTO;

namespace DAL.EF.Repositories;

public class IngredientRepository : EfBaseRepository<Ingredient, ApplicationDbContext>, IIngredientRepository
{
    public IngredientRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public async Task<IEnumerable<IngredientDto>> AllIngredientsAsync()
    {
        return await RepositoryDbSet
         
            .AsNoTracking()
            .Select(recipe => new IngredientDto()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                
                
                
            }).OrderBy(e => e.Name)
            .ToListAsync();
    }

    public async  Task<IngredientDto>? FindIngredientAsync(Guid id)
    {
        return (await RepositoryDbSet
            
            
            .AsNoTracking()
            .Where(m => m.Id == id)
            .Select(recipe => new IngredientDto
            {
                Id = recipe.Id,
                Name = recipe.Name,
               
            })
            .FirstOrDefaultAsync())!;
    }
}

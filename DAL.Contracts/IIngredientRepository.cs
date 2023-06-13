using DAL.Contracts.Base;
using Domain;
using Public.DTO;

namespace DAL.Contracts;

public interface IIngredientRepository: IBaseRepository<Ingredient>
{
    Task<IEnumerable<IngredientDto>> AllIngredientsAsync();
    Task<IngredientDto>? FindIngredientAsync(Guid id);
}

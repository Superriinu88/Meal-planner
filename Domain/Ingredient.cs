using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain;

public class Ingredient: DomainEntityId
{
    [MinLength(1)] [MaxLength(64)] 
    public string Name { get; set; } = default!;
    public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
    public ICollection<UserIngredient>? UserIngredients { get; set; }
}

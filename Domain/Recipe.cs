using System.ComponentModel.DataAnnotations;
using Domain.Base;
using Domain.Identity;

namespace Domain;

public class Recipe : DomainEntityId
{
    [MinLength(1)] [MaxLength(120)] 
    public string Name { get; set; } = default!;
    [MinLength(1)] 
    public string Description { get; set; } = default!;
    [MinLength(1)] 
    public string Instructions { get; set; } = default!;
    public int Servings { get; set; }

    public int PrepTime { get; set; }
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
}

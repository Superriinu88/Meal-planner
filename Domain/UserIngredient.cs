using Domain.Base;
using Domain.Identity;

namespace Domain;

public class UserIngredient: DomainEntityId
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    public Guid IngredientId { get; set; }
    public Ingredient? Ingredient { get; set; }
    public double Quantity { get; set; }
}

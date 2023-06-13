using Domain.Base;

namespace Domain;

public class RecipeIngredient: DomainEntityId
{
    
    public Guid RecipeId { get; set; }
    public Recipe? Recipe { get; set; }
    public Guid IngredientId { get; set; }
    public Ingredient? Ingredient { get; set; }
    public int Quantity { get; set; }
}

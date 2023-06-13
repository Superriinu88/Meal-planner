namespace Public.DTO;

public class RecipeIngredientDto
{
    public Guid? Id { get; set; } = default!;
    public Guid IngredientId { get; set; } 
    public Guid RecipeId { get; set; } 

}

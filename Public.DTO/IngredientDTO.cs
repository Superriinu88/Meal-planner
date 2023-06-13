namespace Public.DTO;

public class IngredientDto
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int Quantity { get; set; } = default!;
    
}
public class IngredientGetDto
{
    
    public string Name { get; set; } = default!;
    public int? Quantity { get; set; }
}

public class IngredientsByUserDto
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;

    
}




namespace Public.DTO;

public class RecipeDto
{
    
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    

    public string Description { get; set; } = default!;

    public string Instructions { get; set; } = default!;
    public int Servings { get; set; }
    public Guid AppUserId { get; set; }

    public int PrepTime { get; set; }
    public IEnumerable<IngredientDto> IngredientsList { get; set; }
}

public class RecipeGetAllDto
{
    
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    
    public int Servings { get; set; }
    
    public int PrepTime { get; set; }

}
public class RecipeGetOneDto
{
    
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    

    public string Description { get; set; } = default!;

    public string Instructions { get; set; } = default!;
    public int Servings { get; set; }


    public int PrepTime { get; set; }
    public IEnumerable<IngredientGetDto> IngredientsList { get; set; }

}


using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;


public class PizzaInputDto
{
    public string? Id { get; set; }

    [Required, MinLength(2), MaxLength(75)]
    public string Name {get; set;}

    [Required, MinLength(2)]
    public string Description {get; set;}

    [Required]
    public SizeCategory SizeCategory {get; set;}   

    [Required, MinLength(1)] 
    public List<PizzaIngredientDto> PizzaIngredients {get; set;}

    [Required, Range(30.0, 2000)]
    public float Price {get; set;}

    [Required, Range(0.0, 1000)]
    public float ProductionCost {get; set;}

    [Required, Range(0.0, 100.0)]
    public float Discount {get; set;}

    [JsonConstructor]
    public PizzaInputDto (
        string name,
        string description,
        string sizeCategory,
        List<PizzaIngredientDto> pizzaIngredients,
        float price,
        float productionCost,
        float discount
    ){
        Name = name;
        Description = description;
        SizeCategory = Enum.Parse<SizeCategory>(sizeCategory);
        PizzaIngredients = pizzaIngredients;
        Price = price;
        ProductionCost = productionCost;
        Discount = discount;
    }
    
    public PizzaInputDto (
        string id,
        string name,
        string description,
        SizeCategory sizeCategory,
        List<PizzaIngredientDto> pizzaIngredients,
        float price,
        float productionCost,
        float discount
    ){
        Id = id;
        Name = name;
        Description = description;
        SizeCategory = sizeCategory;
        PizzaIngredients = pizzaIngredients;
        Price = price;
        ProductionCost = productionCost;
        Discount = discount;
    }
}

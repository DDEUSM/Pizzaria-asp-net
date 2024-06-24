using Newtonsoft.Json;

public class PizzaIngredientDto
{
    public Guid? Id { get; set;}
    public Guid IngredientId { get; set; }
    public string IngredientName { get; set;}
    public float Quantity { get; set; }
    public string Unit { get; set; }

    [JsonConstructor]
    public PizzaIngredientDto (
        Guid ingredientId,
        string ingredientName,
        float quantity,
        string unit
    ){
        IngredientId = ingredientId;
        IngredientName = ingredientName;
        Quantity = quantity;
        Unit = unit;
    }

    public PizzaIngredientDto (
        Guid id,
        Guid ingredientId,
        string ingredientName,
        float quantity,
        string unit
    ): this(ingredientId, ingredientName, quantity, unit)
    {
        Id = id;
    }

}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("PizzaIngredients")]
public class PizzaIngredient
{
    public Guid Id { get; init; }
    public Guid PizzaId { get; init; }
    public Guid IngredientId { get; init; }
    public float Quantity { get; set; }
    public string Unit { get; set; }
    public Pizza Pizza { get; set; }
    public  Ingredient Ingredient { get; set; }

    public PizzaIngredient (
        Guid id,
        Guid pizzaId,
        Guid ingredientId,
        float quantity,
        string unit
    ){
        Id = id;
        PizzaId = pizzaId;
        IngredientId = ingredientId;
        Quantity = quantity;
        Unit = unit;
    }

    
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("PizzaIngredients")]
public class PizzaIngredient
{
    public Guid Id { get; init; }
    public string PizzaId { get; set; }
    public Guid IngredientId { get; set; }
    public string IngredientName { get; set;}
    public float Quantity { get; set; }
    public string Unit { get; set; }
    public Pizza Pizza { get; set; }
    public  Ingredient Ingredient { get; set; }

    public PizzaIngredient (
        Guid id,
        string pizzaId,
        Guid ingredientId,
        string ingredientName,
        float quantity,
        string unit
    ){
        Id = id;
        PizzaId = pizzaId;
        IngredientId = ingredientId;
        IngredientName = ingredientName;
        Quantity = quantity;
        Unit = unit;
    }

    
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table("Ingredients")]
public class Ingredient 
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public float Quantity { get; set; }
    public float UnitPrice { get; set; }
    public string Unit { get; set; }
    public ICollection<PizzaIngredient> PizzaIngredients { get; set; }

    public Ingredient (
        Guid id,
        string name,
        float quantity,
        float unitPrice,
        string unit
    ){
        Id = id;
        Name = name;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Unit = unit;
    }
}
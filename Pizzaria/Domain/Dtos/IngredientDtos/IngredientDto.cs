using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

public class IngredientDto
{
    public Guid? Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public float Quantity { get; set; }

    [Required]
    public float UnitPrice { get; set; }

    [Required]
    public string Unit { get; set; }

    [JsonConstructor]
    public IngredientDto (
        string name,
        float quantity,
        float unitPrice,
        string unit
    ){
        Name = name;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Unit = unit;
    }

     public IngredientDto (
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
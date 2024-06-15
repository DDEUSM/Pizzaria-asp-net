public class Ingredient 
{
    public Guid Id { get; init; }
    public int Position { get; set; }
    public string Name { get; set; }
    public float Quantity { get; set; }
    public float UnitPrice { get; set; }
    public string Unit { get; set; }
    public ICollection<PizzaIngredient> PizzaIngredients { get; set; }

    public Ingredient (
        Guid id,
        int position,
        string name,
        float quantity,
        float unitPrice,
        string unit
    ){
        Id = id;
        Position = position;
        Name = name;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Unit = unit;
    }
}
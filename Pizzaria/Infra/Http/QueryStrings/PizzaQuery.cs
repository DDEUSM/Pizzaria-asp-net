public class PizzaQuery
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? SizeCategory { get; set; } 
    public List<Guid>? IngredientIds { get; set; }   
    public float? Price { get; set; }
    public float? ProductionCost { get; set; }
    public float? Discount { get; set; }    
}
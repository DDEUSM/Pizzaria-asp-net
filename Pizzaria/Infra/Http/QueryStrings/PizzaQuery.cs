public enum PriceSort
{
    max_price_first,
    min_price_first,
}

public class PizzaQuery
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public SizeCategory? SizeCategory { get; set; } 
    public List<string>? Ingredients { get; set; }   
    public float? MaxPrice { get; set; }
    public float? MinPrice { get; set; }
    public PriceSort? PriceSort { get; set; }
    public float? ProductionCost { get; set; }
    public float? MaxDiscount { get; set; }
    public float? MinDiscount { get; set; }
    public DateTime? MaxCreationDate { get; set; }
    public DateTime? MinCreationDate { get; set;}  
}
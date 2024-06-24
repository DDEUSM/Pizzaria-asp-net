public class PizzaOutputDto
{ 
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string SizeCategory { get; set; }
    public List<PizzaIngredientDto> PizzaIngredients { get; set; }
    public float Price { get; set; }
    public float ProductionCost { get; set; }
    public float Discount { get; set; }
    public DateTime CreatedAt { get; set; }

    public PizzaOutputDto (
        string id,
        string name,
        string description,
        string sizeCategory,
        List<PizzaIngredientDto> pizzaIngredients,
        float price,
        float productionCost,
        float discount,
        DateTime createdAt
    ){
        Id = id;
        Name = name;
        Description = description;
        SizeCategory = sizeCategory;
        PizzaIngredients = pizzaIngredients;
        Price = price;
        ProductionCost = productionCost;
        Discount = discount;
        CreatedAt = createdAt;
    }
}

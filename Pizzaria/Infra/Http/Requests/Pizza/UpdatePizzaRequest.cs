public record UpdatePizzaRequest (
    string Name,
    string Description,
    string SizeCategory,    
    List<PizzaIngredient> Ingredients,
    float Price,
    float ProductionCost,
    float Discount,
    int Quantity
);
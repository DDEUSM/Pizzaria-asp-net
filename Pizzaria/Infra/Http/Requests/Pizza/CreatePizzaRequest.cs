
public record CreatePizzaRequest (
    string Name,
    string Description,
    string SizeCategory,    
    List<CreatePizzaIngredientRequest> Ingredients,
    float Price,
    float ProductionCost,
    float Discount,
    int Quantity
);
public record CreatePizzaIngredientRequest (
    Guid IngredientId,
    float Quantity,
    string unit
);
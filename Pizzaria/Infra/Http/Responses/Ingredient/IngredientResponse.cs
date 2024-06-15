public record IngredientResponse
(
    Guid Id, 
    int Position,
    string Name,
    float Quantity,
    float UnitPrice,
    string Unit
);
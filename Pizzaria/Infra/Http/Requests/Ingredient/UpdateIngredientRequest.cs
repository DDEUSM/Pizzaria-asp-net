public record UpdateIngredientRequest
(
    string? Name,
    float? Quantity,
    float? UnitPrice,
    string? Unit
);
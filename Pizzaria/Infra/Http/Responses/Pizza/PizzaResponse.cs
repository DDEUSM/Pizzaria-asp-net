public record PizzaResponse (
    Guid Id,
    int Position,
    string Name,
    string Description,
    string SizeCategory,
    float Price,
    float ProductionCost,
    float Discount,
    int Quantity
);
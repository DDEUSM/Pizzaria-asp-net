using static UtilsMethods;
public class PizzaUseCases: IPizzaUseCases
{    
    private PizzariaDbContext PizzariaContext;
    public PizzaUseCases (        
        PizzariaDbContext pizzariaDbContext
    ){        
        PizzariaContext = pizzariaDbContext?? throw new ArgumentNullException(nameof(PizzariaContext));        
    }

    public Pizza Create(CreatePizzaRequest request)
    {
        var newPizza = new Pizza
        (
            Guid.NewGuid(),
            4,
            request.Name,
            request.Description,
            request.SizeCategory,
            request.Price,
            request.ProductionCost,
            request.Discount,
            request.Quantity
        );

        var foundedPizzas = (from pizza in PizzariaContext.Pizzas
                            where pizza.Name == request.Name && pizza.SizeCategory == request.SizeCategory
                            select pizza).ToList();

        if (foundedPizzas.Count() > 0)
        {
            throw new ApiException (
                message: "Entities Conflicts",
                statusCode: 403    
            );
        }

        var pizzaIngredients = request.Ingredients.Select(ingredient => new PizzaIngredient(
            Guid.NewGuid(),
            newPizza.Id,
            ingredient.IngredientId,
            ingredient.Quantity,
            ingredient.unit
        )).ToList();

        newPizza.PizzaIngredients = pizzaIngredients;
        
        PizzariaContext.Pizzas.Add(newPizza);        
        PizzariaContext.SaveChanges();

        return newPizza;
    }

    public Pizza? GetById(Guid id)
    {
        Pizza? foundedPizza = PizzariaContext.Pizzas.Find(id);

        if (foundedPizza == null)
        {
            throw new ApiException (
                message: "Pizza not founded!",
                statusCode: 404
            );
        }
        
        return foundedPizza;
    }

    public List<Pizza>? Get(PizzaQuery pizzaQuery)
    {        
        return PizzariaContext.Pizzas
            .Where(pizza => pizza.AllPropertiesMatches(pizzaQuery))
            .ToList();
    }

    public void Replace(Guid id, Pizza pizza)
    {
        throw new NotImplementedException();
    }

    public void Update(Guid id, dynamic pizza)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}
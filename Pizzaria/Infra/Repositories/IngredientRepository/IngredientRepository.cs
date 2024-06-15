using System.Reflection;
using static UtilsMethods;
public class IngredientRepository: IIngredientRepository
{
    private PizzariaDbContext context;

    public IngredientRepository(
        PizzariaDbContext context
    ){
        this.context = context;
    }
    public void Create(Ingredient ingredient)
    {
        try 
        {
            context.Ingredients.Add(ingredient);
            context.SaveChanges();
        }
        catch (System.Exception)
        {
            
            throw;
        };
    }
    public Ingredient? GetById(Guid id)
    {
        throw new NotImplementedException();
    }
    public List<Ingredient?> Get(IngredientQuery ingredientQuery)
    {   
        throw new NotImplementedException();
    }
    public void Update(Guid id, UpdateIngredientRequest ingredient)
    {
        throw new NotImplementedException();
    }
    public void Delete (Guid id)
    {
        throw new NotImplementedException();
    }

    public bool GetPizzaIngredients(List<PizzaIngredient> pizzaIngredients)
    {
        throw new NotImplementedException();
    }
}
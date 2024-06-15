
using System.ComponentModel;
using System.Reflection;
using static UtilsMethods;

public class PizzaRepository: IPizzaRepository
{  
    private PizzariaDbContext context;

    public PizzaRepository(PizzariaDbContext context)
    {
        this.context = context?? throw new ArgumentNullException(nameof(context));
    }
    public void Create(Pizza pizza)
    {
        try 
        {
            context.Pizzas.Add(pizza);
            context.PizzaIngredients.AddRange(pizza.PizzaIngredients);
            context.SaveChanges();
        }
        catch (Exception ex)    
        {
            throw new Exception(ex.Message, ex);
        }
    }

    public Pizza? GetById(Guid id)
    {
       throw new NotImplementedException();
    }

    public  List<Pizza> Get(PizzaQuery pizzaQuery)
    {
        throw new NotImplementedException();
    }

    public void Update(Guid id, UpdatePizzaRequest pizza)
    {
        throw new NotImplementedException();   
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}
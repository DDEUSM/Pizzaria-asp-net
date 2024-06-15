public interface IPizzaRepository 
{
    public void Create(Pizza pizza);
    public Pizza? GetById(Guid id);
    public List<Pizza> Get(PizzaQuery pizzaQuery);
    public void Update(Guid id, UpdatePizzaRequest pizza);
    public void Delete(Guid id);
}
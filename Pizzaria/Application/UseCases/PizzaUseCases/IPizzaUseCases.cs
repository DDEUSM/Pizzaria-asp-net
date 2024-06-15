public interface IPizzaUseCases
{
    public Pizza Create(CreatePizzaRequest request);
    public Pizza? GetById(Guid id);
    public List<Pizza>? Get(PizzaQuery pizzaQuery);
    public void Replace(Guid id, Pizza pizza);
    public void Update(Guid id, dynamic pizza);
    public void Delete(Guid id);

}
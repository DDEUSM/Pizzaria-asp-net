public interface IIngredientRepository
{
    public void Create(Ingredient ingredient);
    public Ingredient? GetById(Guid id);
    public bool GetPizzaIngredients(List<PizzaIngredient> IngredientIds);
    public List<Ingredient?> Get(IngredientQuery ingredientQuery);
    public void Update(Guid id, UpdateIngredientRequest ingredient);
    public void Delete (Guid id);
}
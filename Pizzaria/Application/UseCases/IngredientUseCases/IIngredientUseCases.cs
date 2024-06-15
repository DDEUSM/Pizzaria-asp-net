public interface IIngredientUseCases
{
    public void Create(Ingredient newIngredient);
    public Ingredient? GetById(Guid id);
    public List<Ingredient>? Get(IngredientQuery ingredientQuery);
    public void Replace(Guid id, Ingredient ingredient);
    public void Update(Guid id, dynamic ingredient);
    public void Delete(Guid id);

}
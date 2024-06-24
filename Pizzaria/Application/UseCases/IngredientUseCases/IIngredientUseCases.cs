using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public interface IIngredientUseCases
{
    public Ingredient Create(IngredientDto ingredientDto);
    public Ingredient GetById(Guid id);
    public List<Ingredient> Get(IngredientQuery ingredientQuery);
    public void Update(Guid id, JsonPatchDocument<IngredientDto> ingredientPatchDocument, ModelStateDictionary ModelState);
    public void Delete(Guid id);

}
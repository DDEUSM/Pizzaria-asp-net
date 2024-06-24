using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public interface IPizzaUseCases
{
    public Pizza Create(PizzaInputDto pizzaDto);
    public PizzaOutputDto GetById(string id);
    public List<Pizza>? Get(PizzaQuery pizzaQuery);
    public void Update(string id, JsonPatchDocument<PizzaInputDto> pizza, ModelStateDictionary ModelState);
    public void Delete(string id);

}
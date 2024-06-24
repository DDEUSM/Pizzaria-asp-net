
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public class IngredientUseCases : IIngredientUseCases
{
    private PizzariaDbContext PizzariaContext { get; set; }

    public IngredientUseCases (
        PizzariaDbContext context
    ){
        PizzariaContext = context;
    }
    public Ingredient Create(IngredientDto ingredientDto)
    {
        Ingredient newIngredient = new (
            Guid.NewGuid(),
            ingredientDto.Name,
            ingredientDto.Quantity,
            ingredientDto.UnitPrice,
            ingredientDto.Unit
        );
        PizzariaContext.Ingredients.Add(newIngredient);
        PizzariaContext.SaveChanges();

        return newIngredient;
    }
    public Ingredient GetById(Guid id)
    {
        Ingredient? foundedIngredient = PizzariaContext.Ingredients.Find(id);
        if (foundedIngredient == null)
        {
            throw new ApiException(
                message: "Ingredient not founded!",
                statusCode: 404
            );
        }
        return foundedIngredient;
    }
    public List<Ingredient> Get(IngredientQuery ingredientQuery)
    {   
        List<Ingredient> foundedIngredients = PizzariaContext.Ingredients
            .WhereIf(!string.IsNullOrEmpty(ingredientQuery.Name), ingredient => ingredient.Name.Contains(ingredientQuery.Name))
            .WhereIf(ingredientQuery.MinQuantity != null && float.IsPositive((float)ingredientQuery.MinQuantity), ingredient => ingredient.Quantity >= ingredientQuery.MinQuantity)
            .WhereIf(ingredientQuery.MaxQuantity != null && float.IsPositive((float)ingredientQuery.MaxQuantity), ingredient => ingredient.Quantity <= ingredientQuery.MaxQuantity)
            .WhereIf(ingredientQuery.MinUnitPrice != null && float.IsPositive((float)ingredientQuery.MinUnitPrice), ingredient => ingredient.UnitPrice >= ingredientQuery.MinUnitPrice)
            .WhereIf(ingredientQuery.MaxUnitPrice != null && float.IsPositive((float)ingredientQuery.MaxUnitPrice), ingredient => ingredient.UnitPrice <= ingredientQuery.MaxUnitPrice)
            .ToList();
        if (foundedIngredients.Count == 0)
        {
            throw new ApiException (
                message: "Ingredients not founded",
                statusCode: 404
            );
        }

        return foundedIngredients;
    }

    public void Update(Guid id, JsonPatchDocument<IngredientDto> ingredientPatchDocument, ModelStateDictionary ModelState)
    {
        Ingredient? foundedIngredient = PizzariaContext.Ingredients.Find(id);

        if (foundedIngredient == null)
        {
            throw new ApiException(
                message: "Ingredient not founded",
                statusCode: 404
            );
        }

        var ingredientDto = new IngredientDto (
            foundedIngredient.Id,
            foundedIngredient.Name,
            foundedIngredient.Quantity,
            foundedIngredient.UnitPrice,
            foundedIngredient.Unit
        );

        ingredientPatchDocument.ApplyTo(ingredientDto, ModelState);

         if (!ModelState.IsValid)
        {
            throw new ApiException(
                message: "Bad Request",
                statusCode: 400
            );  
        }

        foundedIngredient.SetNewPropertyValues(ingredientDto);
        PizzariaContext.SaveChanges();
    }
    public void Delete (Guid id)
    {
        Ingredient? foundedIngredient = PizzariaContext.Ingredients.Find(id);
        if (foundedIngredient == null)
        {
            throw new ApiException(
                message: "Ingredient not found",
                statusCode: 404
            );
        }
        PizzariaContext.Remove(foundedIngredient);
        PizzariaContext.SaveChanges();
    }

    public bool GetPizzaIngredients(List<PizzaIngredient> pizzaIngredients)
    {
        throw new NotImplementedException();
    }

}
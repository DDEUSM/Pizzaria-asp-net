using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class IngredientsController: ControllerBase
{
    private IIngredientRepository _ingredientRepository;
    public IngredientsController (
        IIngredientRepository ingredientRepository
    ){
        _ingredientRepository = ingredientRepository;
    }

    [HttpPost()]
    public IActionResult Create(CreateIngredientRequest request)
    {
        Ingredient newIngredient = new (
            Guid.NewGuid(),
            0,
            request.Name,
            request.Quantity,
            request.UnitPrice,
            request.Unit
        );

        _ingredientRepository.Create(newIngredient);

        IngredientResponse ingredientResponse = new (
            newIngredient.Id,
            newIngredient.Position,
            newIngredient.Name,
            newIngredient.Quantity,
            newIngredient.UnitPrice,
            newIngredient.Unit
        );

        return CreatedAtAction(
            actionName: nameof(Create),
            routeValues: new {id = newIngredient.Id},
            value: ingredientResponse
        );
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        Ingredient foundedIngredient = _ingredientRepository.GetById(id);
        if (foundedIngredient == null)
        {
            return NotFound();
        }
        var ingredientResponse = new IngredientResponse(
            foundedIngredient.Id,
            foundedIngredient.Position,
            foundedIngredient.Name,
            foundedIngredient.Quantity,
            foundedIngredient.UnitPrice,
            foundedIngredient.Unit
        );
        return Ok(ingredientResponse);
    }

    [HttpGet()]
    public IActionResult Get([FromQuery] IngredientQuery ingredientQuery)
    {
        List<Ingredient> ingredients = _ingredientRepository.Get(ingredientQuery);
        List<IngredientResponse> ingredientsResponse = ingredients.Select(ingredient => new IngredientResponse(
            ingredient.Id,
            ingredient.Position,
            ingredient.Name,
            ingredient.Quantity,
            ingredient.UnitPrice,
            ingredient.Unit
        )).ToList();
        return Ok(ingredientsResponse);
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, UpdateIngredientRequest request)
    {
        _ingredientRepository.Update(id, request);
        return Ok(request);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id) 
    {
        _ingredientRepository.Delete(id);
        return Ok(id);
    }
}
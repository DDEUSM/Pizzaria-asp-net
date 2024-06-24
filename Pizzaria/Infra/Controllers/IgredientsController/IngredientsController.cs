using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class IngredientsController: ControllerBase
{
    private IIngredientUseCases _ingredientUseCases;
    public IngredientsController (
        IIngredientUseCases ingredientUseCases
    ){
        _ingredientUseCases = ingredientUseCases;
    }

    [HttpPost()]
    public IActionResult Create([FromBody] IngredientDto ingredientDto)
    {        
        Ingredient newIngredient = _ingredientUseCases.Create(ingredientDto);

        IngredientDto ingredientResponse = new IngredientDto (
            newIngredient.Id,
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
        Ingredient foundedIngrdient = _ingredientUseCases.GetById(id);
        return Ok(foundedIngrdient);
    }

    [HttpGet()]
    public IActionResult Get([FromQuery] IngredientQuery ingredientQuery)
    {
       List<Ingredient> ingredientsFounded = _ingredientUseCases.Get(ingredientQuery);
       List<IngredientDto> ingredientDtos = ingredientsFounded.Select(ingredient => new IngredientDto(
            ingredient.Id,
            ingredient.Name,
            ingredient.Quantity,
            ingredient.UnitPrice,
            ingredient.Unit
       )).ToList();
       return Ok(ingredientDtos);
    }

    [HttpPatch("{id:guid}")]
    public IActionResult Update(Guid id, JsonPatchDocument<IngredientDto> patch)
    {
         if (patch == null)
        {
            throw new ApiException (
                message: "Bad request patch",
                statusCode: 400
            );
        }
        _ingredientUseCases.Update(id, patch, ModelState);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id) 
    {
        _ingredientUseCases.Delete(id);
        return NoContent();
    }
}
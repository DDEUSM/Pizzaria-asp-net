using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class PizzasController: ControllerBase
{
    private readonly IPizzaUseCases _pizzaUseCases; 
    public PizzasController (
        IPizzaUseCases pizzaUseCases
    ){
        _pizzaUseCases = pizzaUseCases;
    }

    [HttpPost()]
    public IActionResult Create([FromBody] PizzaInputDto pizzaDto)
    {         
        Pizza createdPizza = _pizzaUseCases.Create(pizzaDto);  

        var pizzaIngredientDtos = createdPizza.PizzaIngredients
            .Select(pizzaIngredient => new PizzaIngredientDto (
                id: pizzaIngredient.Id,
                ingredientName: pizzaIngredient.IngredientName,
                ingredientId: pizzaIngredient.IngredientId,
                quantity: pizzaIngredient.Quantity,
                unit: pizzaIngredient.Unit
            )).ToList();  
        
        var newPizzaResponse = new PizzaOutputDto (
                createdPizza.Id,
                createdPizza.Name,
                createdPizza.Description,
                createdPizza.SizeCategory.ToString(),    
                pizzaIngredientDtos,        
                createdPizza.Price,
                createdPizza.ProductionCost,
                createdPizza.Discount,
                createdPizza.CreatedAt
            );

        return CreatedAtAction (
            actionName: nameof(GetById),
            routeValues: new { id = newPizzaResponse.Id },
            value: newPizzaResponse
            );
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {        
        PizzaOutputDto foundedPizza = _pizzaUseCases.GetById(id);

        return Ok(foundedPizza);
    }

    [HttpGet()]
    public IActionResult Get([FromQuery] PizzaQuery pizzaQuery)
    {          
        List<Pizza>? pizzas = _pizzaUseCases.Get(pizzaQuery);

        var pizzasResponse = pizzas.Select(pizza => new PizzaOutputDto (
            pizza.Id,
            pizza.Name,
            pizza.Description,
            pizza.SizeCategory.ToString(),
            new List<PizzaIngredientDto>(),
            pizza.Price,
            pizza.ProductionCost,
            pizza.Discount,
            pizza.CreatedAt   
        ));
        return Ok(pizzasResponse);
    }

    [HttpPatch("{id}")]
    public IActionResult Update(string id, JsonPatchDocument<PizzaInputDto> pizzaPatchDocument)
    {
        if (pizzaPatchDocument == null)
        {
            throw new ApiException (
                message: "Bad request patch",
                statusCode: 400
            );
        }
        _pizzaUseCases.Update(id, pizzaPatchDocument, ModelState);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        _pizzaUseCases.Delete(id);
        return NoContent();
    }
}
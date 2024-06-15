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
    public IActionResult Create(CreatePizzaRequest request)
    {                   
        Pizza result = _pizzaUseCases.Create(request);  
        
        var newPizzaResponse = new PizzaResponse (
                result.Id,
                result.Position,
                result.Name,
                result.Description,
                result.SizeCategory,            
                result.Price,
                result.ProductionCost,
                result.Discount,
                result.Quantity
            );

        return CreatedAtAction (
            actionName: nameof(GetById),
            routeValues: new { id = newPizzaResponse.Id },
            value: newPizzaResponse
            );
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {        
        Pizza? foundedPizza = _pizzaUseCases.GetById(id);

        PizzaResponse response = new PizzaResponse (
            foundedPizza.Id,
            foundedPizza.Position,
            foundedPizza.Name,
            foundedPizza.Description,
            foundedPizza.SizeCategory,
            foundedPizza.Price,
            foundedPizza.ProductionCost,
            foundedPizza.Discount,
            foundedPizza.Quantity
        );

        return Ok(response);
    }

    [HttpGet()]
    public IActionResult Get([FromQuery] PizzaQuery pizzaQuery)
    {          
        try
        {
            List<Pizza>? pizzas = _pizzaUseCases.Get(pizzaQuery);
            return Ok(pizzas);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
            return Ok(pizzaQuery);
        }
    }

    [HttpPatch("{id:guid}")]
    public IActionResult Update(Guid id, UpdatePizzaRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}
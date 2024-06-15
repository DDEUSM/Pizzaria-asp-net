using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class OrdersController: ControllerBase
{    
    public IIngredientRepository _ingredientRepository { get; set; }
    public IPizzaRepository _pizzaRepository { get; set; }
    public IOrderRepository _orderRepository { get; set; }
    public OrdersController (
        IIngredientRepository ingredientRepository,
        IPizzaRepository pizzaRepository,
        IOrderRepository orderRepository
    ){
        _ingredientRepository = ingredientRepository;
        _pizzaRepository = pizzaRepository;
        _orderRepository = orderRepository;
    }

    [HttpPost()]
    public IActionResult Create(CreateOrderRequest request)
    {
        // verify if ingredients are disposable to make this pizza        
       throw new NotImplementedException();
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpGet()]
    public IActionResult Get([FromQuery] OrderQuery orderQuery)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, UpdateOrderRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id) 
    {
        throw new NotImplementedException();
    }
}
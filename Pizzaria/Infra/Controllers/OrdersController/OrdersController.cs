using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class OrdersController: ControllerBase
{    
    private OrderUseCases _orderUseCases { get; set; }
    public OrdersController (
       OrderUseCases orderUseCases
    ){
        _orderUseCases = orderUseCases;
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
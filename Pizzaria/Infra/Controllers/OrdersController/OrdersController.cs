using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class OrdersController: ControllerBase
{    
    private IOrderUseCases _orderUseCases { get; set; }
    public OrdersController (
       IOrderUseCases orderUseCases
    ){
        _orderUseCases = orderUseCases;
    }

    [HttpPost()]
    public IActionResult Create([FromBody] OrderInputDto orderInputDto)
    {  
        Order newOrder = _orderUseCases.Create(orderInputDto);      

        List<OrderPizzaDto> orderPizzaDtos = newOrder.OrderPizzas
            .Select(orderPizza => new OrderPizzaDto {
                Id = orderPizza.Id,
                OrderId = orderPizza.OrderId,
                FirstPizzaId = orderPizza.FirstPizzaId,
                SecondPizzaId = orderPizza.SecondPizzaId
            }).ToList();
        
        OrderOutputDto orderOutputDto = new OrderOutputDto(
            newOrder.Id,
            newOrder.TableId,
            newOrder.CommandId,
            newOrder.TotalPrice,
            newOrder.CreatedAt,
            orderPizzaDtos
        );

        return CreatedAtAction (
            actionName: nameof(GetById),
            routeValues: new { Id = newOrder.Id },
            value: orderOutputDto
        );
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        Order foundedOrder = _orderUseCases.GetById(id);
        return Ok(foundedOrder);
    }

    [HttpGet()]
    public IActionResult Get([FromQuery] OrderQuery orderQuery)
    {
        List<Order> foundedOrders = _orderUseCases.Get(orderQuery);
        var orderOutputDtos = foundedOrders.Select(order => new OrderOutputDto (
                order.Id,
                order.TableId,
                order.CommandId,
                order.TotalPrice,
                order.CreatedAt,
                order.OrderPizzas.Select(orderPizza => new OrderPizzaDto {
                        Id = orderPizza.Id,
                        OrderId = orderPizza.OrderId,
                        FirstPizzaId = orderPizza.FirstPizzaId,
                        SecondPizzaId = orderPizza.SecondPizzaId
                    }).ToList()
                ));
        return Ok(orderOutputDtos);
    }

    [HttpPatch("{id:guid}")]
    public IActionResult Update(Guid id, JsonPatchDocument<OrderInputDto> patch)
    {
        if (patch == null)
        {
            throw new ApiException (
                message: "Bad Request",
                statusCode: 400
                );
        }

        _orderUseCases.Update(id, patch, ModelState);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id) 
    {
        _orderUseCases.Delete(id);
        return NoContent();
    }
}
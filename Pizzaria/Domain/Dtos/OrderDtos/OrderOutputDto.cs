using System.ComponentModel.DataAnnotations;

public class OrderOutputDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public int TableId { get; set; }

    [Required]
    public int CommandId { get; set; }

    [Required, MinLength(1)]
    public List<OrderPizzaDto> PizzaOrders { get; set; }  

    [Required]
    public float TotalPrice { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

      public OrderOutputDto(
        Guid id,
        int tableId,
        int commandId,
        float totalPrice,
        DateTime createdAt,
        List<OrderPizzaDto> pizzaOrders
    ){
        Id = id;
        TableId = tableId;
        CommandId = commandId;
        TotalPrice = totalPrice;
        CreatedAt = createdAt;
        PizzaOrders = pizzaOrders;
    } 
}
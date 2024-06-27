using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http.Features;

[Table("Orders")]
public class Order 
{
    public Guid Id { get; init; }
    public int TableId { get; set; }
    public int CommandId { get; set; }    
    public float TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<OrderPizza> OrderPizzas { get; set; }

    public Order()
    {
        OrderPizzas = new HashSet<OrderPizza>();
    }
    public Order (
        Guid id,
        int tableId,
        int commandId,
        float totalPrice,
        DateTime createdAt   
    ) : this()
    {
        Id = id;
        TableId = tableId;
        CommandId = commandId;     
        TotalPrice = totalPrice;   
        CreatedAt = createdAt;
    }
}
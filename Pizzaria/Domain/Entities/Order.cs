using Microsoft.AspNetCore.Http.Features;

public class Order 
{
    public Guid Id { get; init; }
    public int Position { get; set; }
    public int TableId { get; set; }
    public int CommandId { get; set; }    
    public ICollection<Pizza> Pizzas { get; set; }

    public Order()
    {
        Pizzas = new HashSet<Pizza>();
    }
    public Order (
        Guid id,
        int position,
        int tableId,
        int command        
    ) : this()
    {
        Id = id;
        Position = position;
        TableId = tableId;
        CommandId = command;        
    }
}
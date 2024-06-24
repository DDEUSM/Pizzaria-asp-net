using System.ComponentModel.DataAnnotations.Schema;

[Table("OrderPizzas")]
public class OrderPizza
{
    public Guid Id { get; set;}
    public Guid OrderId { get; set;}
    public string FirstPizzaId { get; set;}
    public Pizza FirstPizza { get; set;}
    public string SecondPizzaId { get; set;}
    public Pizza SecondPizza { get; set;}
    public Order Order { get; set;}

    public OrderPizza (
        Guid id,
        Guid orderId,
        string firstPizzaId,
        string secondPizzaId
    ){
        Id = id;
        OrderId = orderId;
        FirstPizzaId = firstPizzaId;
        SecondPizzaId = secondPizzaId;
    }
}
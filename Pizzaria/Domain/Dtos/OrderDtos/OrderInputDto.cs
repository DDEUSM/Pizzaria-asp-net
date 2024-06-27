using System.ComponentModel.DataAnnotations;

public class OrderInputDto
{
  [Required]
  public int TableId { get; set; }

  [Required]
  public int CommandId { get; set; }

  [Required, MinLength(1)]
  public List<OrderPizzaDto> PizzaOrders { get; set; }  

  public Dictionary<string, int> PizzaMap { get; set; }

  public OrderInputDto (
    int tableId,
    int commandId,
    List<OrderPizzaDto> pizzaOrders
  ){
    TableId = tableId;
    CommandId = commandId;
    PizzaOrders = pizzaOrders;
    PizzaMap = new Dictionary<string, int>();
    pizzaOrders.ForEach(pizza => { 
        int value1;
        PizzaMap[pizza.FirstPizzaId] = PizzaMap.TryGetValue(pizza.FirstPizzaId, out value1)? PizzaMap[pizza.FirstPizzaId] + 1 : 1;
        PizzaMap[pizza.SecondPizzaId] = PizzaMap.TryGetValue(pizza.SecondPizzaId, out value1)? PizzaMap[pizza.SecondPizzaId] + 1 : 1;
    });
  }    
}
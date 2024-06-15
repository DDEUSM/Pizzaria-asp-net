using static UtilsMethods;
public class OrderRepository : IOrderRepository
{
    private static Dictionary<Guid, Order> OrderDb = new ();
    public void Create(Order order)
    {
        OrderDb.Add(order.Id, order);        
    }

    public void Delete(Guid id)
    {
        OrderDb.Remove(id);
    }

    public List<Order> Get(OrderQuery orderQuery)
    {
        return OrderDb.Values
            .ToList()
            .FindAll(order => order.AllPropertiesMatches(orderQuery));
    }

    public Order? GetById(Guid id)
    {
        Order? foundedOrder;
        OrderDb.TryGetValue(id, out foundedOrder);
        return foundedOrder;
    }

    public void Update(Guid id, UpdateOrderRequest order)
    {
        Order? foundedOrder;
        OrderDb.TryGetValue (id, out foundedOrder);
        if (foundedOrder == null)
        {
            return; 
        }
        SetNewPropertyValues(order, foundedOrder);
    }   
}
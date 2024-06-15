public interface IOrderRepository
{
    public void Create(Order order);
    public Order? GetById(Guid id);
    public List<Order> Get(OrderQuery orderQuery);
    public void Update(Guid id, UpdateOrderRequest order);
    public void Delete (Guid id);
}
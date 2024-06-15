public interface IOrderUseCases
{
    public void Create(Order order);   
    public Order? GetById(Guid id);
    public List<Order>? Get(OrderQuery orderQuery);
    public void Replace(Guid id, Order order);
    public void Update(Guid id, dynamic order);
    public void Delete(Guid id);
}
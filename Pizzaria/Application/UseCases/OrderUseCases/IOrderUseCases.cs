using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public interface IOrderUseCases
{
    public Order Create(OrderInputDto orderInputDto);   
    public Order? GetById(Guid id);
    public List<Order>? Get(OrderQuery orderQuery);
    public void Update(Guid id, JsonPatchDocument<OrderInputDto> patch, ModelStateDictionary ModelState);
    public void Delete(Guid id);
}
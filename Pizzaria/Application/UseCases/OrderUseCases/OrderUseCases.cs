
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

public class OrderUseCases : IOrderUseCases
{
  private PizzariaDbContext PizzariaContext { get; set; }
  public OrderUseCases(
      PizzariaDbContext pizzariaDbContext
  )
  {
    PizzariaContext = pizzariaDbContext;
  }

  public Order Create(OrderInputDto orderInputDto)
  {
    List<FieldError> fieldErrors = new();
    var pizzasIds = orderInputDto.PizzaMap.Keys.ToList();

    var isAvailable = PizzariaContext.PizzaIngredients
        .Include(entity => entity.Ingredient)
        .Where(pizzaIngredient => pizzasIds.Contains(pizzaIngredient.PizzaId))
        .Select(pizzaIngredient => new
        {
          PizzaId = pizzaIngredient.PizzaId,
          IngredientId = pizzaIngredient.IngredientId,
          IngredientName = pizzaIngredient.IngredientName,
          Quantity = pizzaIngredient.Quantity * orderInputDto.PizzaMap[pizzaIngredient.PizzaId],
          Ingredient = pizzaIngredient.Ingredient
        })
        .ToList()
        .GroupBy(pizzaIngredient => new
        {
          pizzaIngredient.IngredientId,
          pizzaIngredient.IngredientName,
          pizzaIngredient.Ingredient
        })
        .Aggregate(true, (available, item) =>
        {
          float quantityRest = item.Key.Ingredient.Quantity - item.Sum(x => x.Quantity);
          if (!available)
          {
            var isAvailable = float.IsPositive(quantityRest);
            if (!isAvailable)
            {
              fieldErrors.Add(new FieldError
              {
                FieldName = item.Key.IngredientName,
                Message = "Insuficient quantity of ingredients"
              });
            }
            return false;
          }
          var ingredient = PizzariaContext.Ingredients.Find(item.Key.IngredientId);
          if (ingredient != null)
          {
            ingredient.Quantity = quantityRest;
            PizzariaContext.Ingredients.Update(ingredient);
          }
          return float.IsPositive(quantityRest);
        });

    if (!isAvailable)
    {
      throw new ApiException(
          message: "Insuficient quantity of ingredients",
          statusCode: 403,
          fieldErrors: fieldErrors
      );
    }

    float totalPrice = PizzariaContext.Pizzas
      .Where(pizza => pizzasIds.Contains(pizza.Id))
      .Select(pizza => new {
        PizzaPrice = pizza.Price * orderInputDto.PizzaMap[pizza.Id]  
      })
      .ToList()
      .Sum(pizza => pizza.PizzaPrice);

    Order newOrder = new Order(
        Guid.NewGuid(),
        orderInputDto.TableId,
        orderInputDto.CommandId,
        totalPrice,
        DateTime.Now
    );

    List<OrderPizza> OrderPizzas = orderInputDto.PizzaOrders.Select(
        pizzaOrder => new OrderPizza(
            Guid.NewGuid(),
            newOrder.Id,
            pizzaOrder.FirstPizzaId,
            pizzaOrder.SecondPizzaId
        )).ToList();

    newOrder.OrderPizzas = OrderPizzas;
    PizzariaContext.Orders.Add(newOrder);
    PizzariaContext.SaveChanges();

    return newOrder;
  }

  public void Delete(Guid id)
  {
        Order? foundedOrder = PizzariaContext.Orders.Find(id);

        if (foundedOrder == null)
        {
            throw new ApiException(
                message: "Order not founded",
                statusCode: 404
                );
        }
        PizzariaContext.Orders.Remove(foundedOrder);
        PizzariaContext.SaveChanges(true);
  }

  public List<Order> Get(OrderQuery orderQuery)
  {
        List<Order> foundedOrders = PizzariaContext.Orders
            .Include(p => p.OrderPizzas)
            .WhereIf(
                orderQuery.CommandId != null && int.IsPositive((int)orderQuery.CommandId), 
                oneOrder => oneOrder.CommandId == orderQuery.CommandId
            )
            .WhereIf(
                orderQuery.TableId != null && int.IsPositive((int) orderQuery.TableId),
                order => order.TableId.Equals(orderQuery.TableId)
            )
            .ToList();

        if (foundedOrders.Count() < 1)
        {
            throw new ApiException(
                message: "Orders not founded",
                statusCode: 404
                );
        }

        return foundedOrders;
  }

  public Order GetById(Guid id)
  {
        Order? foundedOrder = PizzariaContext.Orders.Find(id);

        if (foundedOrder == null)
        {
            throw new ApiException(
                message: "Order not founded",
                statusCode: 404
                );
        }

        return foundedOrder;
  }

 
  public void Update(Guid id, JsonPatchDocument<OrderInputDto> patch, ModelStateDictionary ModelState)
  {
        Order? foundedOrder = PizzariaContext.Orders.Find(id);

        if (foundedOrder == null)
        {
            throw new ApiException(
               message: "Order not founded",
               statusCode: 404
               );
        }

        OrderInputDto orderInputDto = new OrderInputDto(
            foundedOrder.TableId,
            foundedOrder.CommandId,
            foundedOrder.OrderPizzas.Select(orderPizza => new OrderPizzaDto {
                    Id = orderPizza.Id,
                    OrderId = orderPizza.OrderId,
                    FirstPizzaId = orderPizza.FirstPizzaId,
                    SecondPizzaId = orderPizza.SecondPizzaId
                }).ToList()
            );

        patch.ApplyTo(orderInputDto, ModelState);

        if (!ModelState.IsValid)
        {
            throw new ApiException(
               message: "Bad Request",
               statusCode: 400
               );
        }

        foundedOrder.TableId = orderInputDto.TableId;
        foundedOrder.CommandId = orderInputDto.CommandId;

        PizzariaContext.SaveChanges();
  }
}
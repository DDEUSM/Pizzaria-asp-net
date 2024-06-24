using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;


public class PizzaUseCases: IPizzaUseCases
{    
    private PizzariaDbContext PizzariaContext;
    public PizzaUseCases (        
        PizzariaDbContext pizzariaDbContext
    ){        
        PizzariaContext = pizzariaDbContext?? throw new ArgumentNullException(nameof(PizzariaContext));        
    }

    public Pizza Create(PizzaInputDto pizzaDto)
    {
        List<PizzaIngredient> pizzaIngredients = new();

        var newPizza = new Pizza
        (
            pizzaDto.Name.ToLower().Replace(" ", "")+"-"+pizzaDto.SizeCategory,
            pizzaDto.Name,
            pizzaDto.Description,
            pizzaDto.SizeCategory,
            pizzaDto.Price,
            pizzaDto.ProductionCost,
            pizzaDto.Discount,
            DateTime.Now
        );

        var foundedPizza = PizzariaContext.Pizzas.Find(newPizza.Id);

        if (foundedPizza != null)
        {
            throw new ApiException (
                message: "Entities Conflicts",
                statusCode: 403    
            );
        }
    
        var ingredientsIds = pizzaDto.PizzaIngredients.Select(ingredient => {
            pizzaIngredients.Add( new PizzaIngredient (
                Guid.NewGuid(),
                newPizza.Id,
                ingredient.IngredientId,
                ingredient.IngredientName,
                ingredient.Quantity,
                ingredient.Unit
            ));
            return ingredient.IngredientId;
        }).ToList();

        var foundedIngredients = PizzariaContext.Ingredients.Where(ingredient => ingredientsIds.Contains(ingredient.Id));

        if (foundedIngredients.Count() < ingredientsIds.Count())
        {
            throw new ApiException (
                message: "Ingredients Not Founded",
                statusCode: 404
            );
        }

        newPizza.PizzaIngredients = pizzaIngredients;
        
        PizzariaContext.Pizzas.Add(newPizza);  
             
        PizzariaContext.SaveChanges();

        return newPizza;
    }

    public PizzaOutputDto GetById(string id)
    {
        Pizza? foundedPizza = PizzariaContext.Pizzas.Find(id);

        if (foundedPizza == null)
        {
            throw new ApiException (
                message: "Pizza not founded!",
                statusCode: 404
            );
        }

        var pizzaIngredients = (from pizza in PizzariaContext.Pizzas
                                join pizzaIngredient in PizzariaContext.PizzaIngredients 
                                on pizza.Id equals pizzaIngredient.PizzaId
                                
                                select new PizzaIngredientDto (
                                    pizzaIngredient.Id,
                                    pizzaIngredient.IngredientId,
                                    pizzaIngredient.IngredientName,
                                    pizzaIngredient.Quantity,
                                    pizzaIngredient.Unit
                                )).ToList();
        
        return new PizzaOutputDto (
            id: foundedPizza.Id,
            name: foundedPizza.Name,
            description: foundedPizza.Description,
            sizeCategory: foundedPizza.SizeCategory.ToString(),
            pizzaIngredients: pizzaIngredients,
            price: foundedPizza.Price,
            productionCost: foundedPizza.ProductionCost,
            discount: foundedPizza.Discount,
            createdAt: foundedPizza.CreatedAt
        );
    }

    public List<Pizza>? Get(PizzaQuery pizzaQuery)
    {        
        /* List<Pizza> pizzas = PizzariaContext.Pizzas
            .Join(
                PizzariaContext.PizzaIngredients,
                pizza => pizza.Id,
                pizzaIngredient => pizzaIngredient.PizzaId,
                (pizza, PizzaIngredient) => new {
                    pizza.Id,
                    pizza.Name,
                    pizza.Description,
                    pizza.SizeCategory,
                    pizza.Price,
                    pizza.ProductionCost,
                    pizza.Discount,
                    pizza.CreatedAt
                    pizza.
                }
            )
            .WhereIf(pizzaQuery.SizeCategory != null, pizza => pizza.SizeCategory.ToString().Contains(pizzaQuery.SizeCategory.ToString()))  
            .WhereIf(pizzaQuery.IngredientIds.Count > 0, pizza => pizza())             
            .ToList();
        */
        List<Pizza> pizzas = PizzariaContext.Pizzas.Include(e => e.PizzaIngredients)
            .WhereIf(!string.IsNullOrEmpty(pizzaQuery.Name), pizza => pizza.Name.Contains(pizzaQuery.Name))
            .WhereIf(!string.IsNullOrEmpty(pizzaQuery.Description), pizza => pizza.Description.Contains(pizzaQuery.Description))
            .WhereIf(pizzaQuery.SizeCategory != null, pizza => pizza.SizeCategory.Equals(pizzaQuery.SizeCategory))
            .WhereIf(pizzaQuery.MinPrice != null && float.IsPositive((float)pizzaQuery.MinPrice), pizza => pizza.Price >= pizzaQuery.MinPrice)               
            .WhereIf(pizzaQuery.MaxPrice != null && float.IsPositive((float)pizzaQuery.MaxPrice), pizza => pizza.Price <= pizzaQuery.MaxPrice)
            .WhereIf(pizzaQuery.MinDiscount != null && float.IsPositive((float)pizzaQuery.MinDiscount) , pizza => pizza.Discount >= pizzaQuery.MinDiscount )
            .WhereIf(pizzaQuery.MaxDiscount != null && float.IsPositive((float)pizzaQuery.MaxDiscount), pizza => pizza.Discount <= pizzaQuery.MaxDiscount )
            .WhereIf(pizzaQuery.MinCreationDate != null && pizzaQuery.MinCreationDate >= DateTime.MinValue, pizza => pizza.CreatedAt >= pizzaQuery.MinCreationDate)             
            .WhereIf(pizzaQuery.MaxCreationDate != null && pizzaQuery.MaxCreationDate < DateTime.Now, pizza => pizza.CreatedAt <= pizzaQuery.MaxCreationDate)
            .WhereIf(pizzaQuery.Ingredients != null && pizzaQuery.Ingredients.Count > 0, pizza => pizza.PizzaIngredients.Any(ingredient => pizzaQuery.Ingredients.Contains(ingredient.IngredientName)))
            .ToList();
    
        if (pizzas.Count == 0)
        {
            throw new ApiException(
                message: "Pizzas not founded!",
                statusCode: 404
            );
        }
        
        return pizzas;
    }

    public void Update(string id, JsonPatchDocument<PizzaInputDto> pizzaPatchDocument, ModelStateDictionary ModelState)
    {
        Pizza? foundedPizza = PizzariaContext.Pizzas.Find(id);

        if (foundedPizza == null)
        {
            throw new ApiException (
                message: "Not found",
                statusCode: 404
            );
        }

        var pizzaIngredientsUpdate = foundedPizza.PizzaIngredients
            .Select(pizzaIngredient => new PizzaIngredientDto (
                id: pizzaIngredient.Id,
                ingredientName: pizzaIngredient.IngredientName,
                ingredientId: pizzaIngredient.IngredientId,
                quantity: pizzaIngredient.Quantity,
                unit: pizzaIngredient.Unit
            )).ToList();

        var pizzaDto = new PizzaInputDto (
            foundedPizza.Id,
            foundedPizza.Name,
            foundedPizza.Description,
            foundedPizza.SizeCategory,
            pizzaIngredientsUpdate,
            foundedPizza.Price,
            foundedPizza.ProductionCost,
            foundedPizza.Discount
        );

        pizzaPatchDocument.ApplyTo(pizzaDto, ModelState);

        if (!ModelState.IsValid)
        {
            throw new ApiException(
                message: "Bad Request",
                statusCode: 400
            );  
        }

        foundedPizza.PizzaIngredients.Select(ingredient => {
            var index = -1;
            var ingredientUpdate = pizzaIngredientsUpdate.Find(ingredientUpdate => {
                index ++;
                return ingredientUpdate.IngredientId == ingredient.Id;
            });

            if (index == -1)
            {
                return ingredient;
            }

            ingredient.IngredientId = ingredientUpdate.IngredientId;
            ingredient.PizzaId = pizzaDto.Id;
            ingredient.Quantity = ingredientUpdate.Quantity;
            ingredient.Unit = ingredientUpdate.Unit;

            pizzaIngredientsUpdate.RemoveAt(index);
            return ingredient;
        });

        foundedPizza.Name = pizzaDto.Name;
        foundedPizza.Description = pizzaDto.Description;
        foundedPizza.SizeCategory = pizzaDto.SizeCategory;
        foundedPizza.Price = pizzaDto.Price;
        foundedPizza.ProductionCost = pizzaDto.ProductionCost;
        foundedPizza.Discount = pizzaDto.Discount;

        PizzariaContext.SaveChanges();
    }

    public void Delete(string id)
    {
        Pizza? foundedPizza = PizzariaContext.Pizzas.Find(id);
        if (foundedPizza == null)
        {
            throw new ApiException(
                message: "Pizza Not Founded",
                statusCode: 404
            );
        }  
        PizzariaContext.Pizzas.Remove(foundedPizza);
        PizzariaContext.SaveChanges();
    }

    public void DeleteMany(List<string> ids)
    {
        List<Pizza>? pizzas = PizzariaContext.Pizzas
            .Where(pizzas => ids.Contains(pizzas.Id))
            .ToList();
        
        if (pizzas.Count == 0)
        {
            throw new ApiException(
                message: "Pizzas not founded",
                statusCode: 404
            );
        }
        PizzariaContext.RemoveRange(pizzas);
        PizzariaContext.SaveChanges();
    }
}
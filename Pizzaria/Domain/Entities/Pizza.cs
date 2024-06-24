using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public enum SizeCategory {
    family,
    medium,
    small
};

[Table("Pizzas")]
public class Pizza
{    
    public string Id { get; init; }
    public string Name { get; set; }
    public string Description { get; set; }
    public SizeCategory SizeCategory { get; set; }         
    public float Price { get; set; }
    public float ProductionCost { get; set; }
    public float Discount { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<OrderPizza> OrderPizzas1 { get; set; }
    public ICollection<OrderPizza> OrderPizzas2 { get; set; }
    public ICollection<PizzaIngredient> PizzaIngredients { get; set; }

    public Pizza()
    {
        OrderPizzas1 = new HashSet<OrderPizza>();
        OrderPizzas2 = new HashSet<OrderPizza>();
        PizzaIngredients = new HashSet<PizzaIngredient>();
    }

    public Pizza (
        string id,
        string name,
        string description,
        SizeCategory sizeCategory,        
        float price,
        float productionCost,
        float discount,
        DateTime createdAt
    ) : this()
    {
        Id = id;
        Name = name;
        Description = description;
        SizeCategory = sizeCategory;
        Price = price;
        ProductionCost = productionCost;
        Discount = discount;   
        CreatedAt = createdAt;
    }
}
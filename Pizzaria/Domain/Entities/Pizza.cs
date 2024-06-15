using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Pizzas")]
public class Pizza
{    
    public Guid Id { get; init; }
    public int Position { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string SizeCategory { get; set; }         
    public float Price { get; set; }
    public float ProductionCost { get; set; }
    public float Discount { get; set; }
    public int Quantity { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<PizzaIngredient> PizzaIngredients { get; set; }

    public Pizza()
    {
        Orders = new HashSet<Order>();
        PizzaIngredients = new HashSet<PizzaIngredient>();
    }

    public Pizza (
        Guid id,
        int position,
        string name,
        string description,
        string sizeCategory,        
        float price,
        float productionCost,
        float discount,
        int quantity
    ) : this()
    {
        Id = id;
        Position = position;
        Name = name;
        Description = description;
        SizeCategory = sizeCategory;
        Price = price;
        ProductionCost = productionCost;
        Discount = discount;
        Quantity = quantity;        
    }
}
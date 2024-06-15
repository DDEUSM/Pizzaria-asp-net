using Microsoft.EntityFrameworkCore;

public class PizzariaDbContext: DbContext
{
    private IConfiguration _configuration;
    public DbSet<Pizza> Pizzas { get; set; }
    private DbSet<Order> Orders { get; set; }
    public DbSet<PizzaIngredient> PizzaIngredients { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public PizzariaDbContext(IConfiguration configuration, DbContextOptions options) : base(options)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var typeDatabase = _configuration["TypeDatabase"];
        var connectionString = _configuration.GetConnectionString(typeDatabase);

        switch(typeDatabase)
        {
            case "SqlServer":
                optionsBuilder.UseSqlServer(connectionString);
                break;
            case "Postgresql":
                // UseNpgsql
                break;
        }
    }
    
}
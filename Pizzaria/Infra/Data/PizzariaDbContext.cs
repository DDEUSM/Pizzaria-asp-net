using Microsoft.EntityFrameworkCore;

public class PizzariaDbContext: DbContext
{
    private IConfiguration _configuration;
    public DbSet<Pizza> Pizzas { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderPizza> OrderPizzas { get; set; }
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pizza>(entity => 
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.SizeCategory)
            .HasConversion(
                s => s.ToString(),
                s => (SizeCategory)Enum.Parse(typeof(SizeCategory), s)
            );
        });

        modelBuilder.Entity<OrderPizza>(entity => 
        {
            entity.HasOne(p => p.Order)
            .WithMany(p => p.OrderPizzas)
            .HasForeignKey(p => p.OrderId);

            entity.HasOne(p => p.FirstPizza)
            .WithMany(p => p.OrderPizzas1)
            .HasForeignKey(p => p.FirstPizzaId)
            .OnDelete(DeleteBehavior.Restrict);

             entity.HasOne(p => p.SecondPizza)
            .WithMany(p => p.OrderPizzas2)
            .HasForeignKey(p => p.SecondPizzaId)
            .OnDelete(DeleteBehavior.Restrict);
            
        });       
    }

}
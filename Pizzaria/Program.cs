
var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddDbContext<PizzariaDbContext>();    
    builder.Services.AddControllers(options => 
        options.Filters.Add<ErrorHandlingFilterAttribute>()
    ).AddNewtonsoftJson();
    builder.Services.AddScoped<IPizzaUseCases, PizzaUseCases>();
    builder.Services.AddScoped<IIngredientUseCases, IngredientUseCases>();
    builder.Services.AddScoped<IOrderUseCases, OrderUseCases>();    
}

var app = builder.Build();
{    
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}

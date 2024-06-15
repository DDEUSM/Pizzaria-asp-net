using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddDbContext<PizzariaDbContext>();    
    builder.Services.AddControllers(options => 
        options.Filters.Add<ErrorHandlingFilterAttribute>()
    );
    builder.Services.AddScoped<IPizzaUseCases, PizzaUseCases>();
    builder.Services.AddScoped<IPizzaRepository,PizzaRepository>();
    builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
    builder.Services.AddScoped<IOrderRepository, OrderRepository>();    
}

var app = builder.Build();
{    
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
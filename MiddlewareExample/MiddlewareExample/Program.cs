var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.Run(async context =>
{
    await context.Response.WriteAsync("Hello from the middleware component");
});

app.Use(async (context, next) =>
{
    await Console.Out.WriteLineAsync($"Logic before executing the next delegate in the Use method");
    await next();
    await Console.Out.WriteLineAsync($"Logic after executing the next delegate in the Use method");
});

app.MapControllers();

app.Run();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();


app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("Hello from the middleware component.");
    await next();
    Console.WriteLine($"Logic after executing the next delegate in the Use method");
});
app.Map("/usingmapbranch", builder =>
{
    builder.Use(async (context, next) =>
    {
        Console.WriteLine("Map branch USE logic before the call of next delegate");
        await next();
        Console.WriteLine("Map branch USE logic after the call of next delegate");
    });
    builder.Run(async context =>
    {
        Console.WriteLine("Map branch RUN logic");
        await context.Response.WriteAsync("Hello from the middleware map branch");
    });
});
app.Run(async context =>
{
    Console.WriteLine($"Writing the response to the client in the Run method");
    context.Response.StatusCode = 200;
    await context.Response.WriteAsync("Hello from the middleware component.");
});
app.MapControllers();

app.Run();
  
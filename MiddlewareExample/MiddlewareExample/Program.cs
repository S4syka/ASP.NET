var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();


app.Use(async (context, next) =>
{
    await Console.Out.WriteLineAsync($"Logic before executing the next delegate in the Use method");
    await next();
    await Console.Out.WriteLineAsync($"Logic after executing the next delegate in the Use method");
});
app.Run(async context =>
{
    await Console.Out.WriteLineAsync($"Writing the respone in the Run method");
    await context.Response.WriteAsync("Hello from the last middleware component");
});

app.MapControllers();

app.Run();

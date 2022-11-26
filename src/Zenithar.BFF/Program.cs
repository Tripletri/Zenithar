using System.Reflection;
using Zenithar.BFF.Clients.Products;
using Zenithar.BFF.Repositories;
using Zenithar.BFF.Services;
using Zenithar.BFF.WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllers(o =>
{
    o.Filters.Add<BasketContextActionFilter>();
    o.Filters.Add<ExceptionFilter>();
});
services.AddSwaggerGen();

services.Configure<ProductsClientOptions>(builder.Configuration.GetRequiredSection("ProductsClient"));

services.AddAutoMapper(Assembly.GetExecutingAssembly());
services.AddSingleton<IProductsClient, ProductsClient>();
services.AddSingleton<IBasketService, BasketService>();
services.AddSingleton<IBasketRepository, InMemoryBasketRepository>();
services.AddSingleton<HttpClient>();

// ---------------------
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.MapFallbackToFile("index.html");

app.Run();
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Zenithar.ProductsAPI.Application;
using Zenithar.ProductsAPI.DataAccess;
using Zenithar.ProductsAPI.Options;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.Configure<DbStartUpOptions>(builder.Configuration.GetRequiredSection("DbStartUp"));

services.AddControllers();
services.AddSwaggerGen();

services.AddDbContext<ProductsDbContext>(o =>
{
    o.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});

services.AddHostedService<DbStartUpService>();

services.AddAutoMapper(Assembly.GetExecutingAssembly());

services.AddScoped<IProductsService, ProductsService>();

// ---------------------
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

await app.RunAsync();
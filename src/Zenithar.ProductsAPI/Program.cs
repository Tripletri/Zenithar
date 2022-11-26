using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Zenithar.ProductsAPI.Application;
using Zenithar.ProductsAPI.DataAccess;
using Zenithar.ProductsAPI.Options;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.Configure<PostgresOptions>(builder.Configuration.GetRequiredSection("Postgres"));
services.Configure<DbStartUpOptions>(builder.Configuration.GetRequiredSection("DbStartUp"));

services.AddControllers();
services.AddSwaggerGen();

services.AddDbContext<ProductsDbContext>(o =>
{
    o.UseNpgsql();
});

services.AddHostedService<DbStartUpService>();

services.AddAutoMapper(Assembly.GetExecutingAssembly());

services.AddScoped<IProductsService, ProductsService>();

// ---------------------
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();

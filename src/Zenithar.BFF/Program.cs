using System.Reflection;
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using Microsoft.Extensions.Options;
using Zenithar.BFF.Clients.Products;
using Zenithar.BFF.Configuration;
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
services.AddSingleton<IProductsService, ProductsService>();
services.AddSingleton<HttpClient>();

// AWS
services.AddOptions<ApplicationAWSOptions>()
    .BindConfiguration("AWS");

services.AddDefaultAWSOptions(x =>
{
    var options = x.GetRequiredService<IOptions<ApplicationAWSOptions>>().Value;

    AWSConfigs.AWSRegion = "ru-central1";

    return new AWSOptions
    {
        Credentials = new BasicAWSCredentials(options.AccessKeyId, options.AccessKeySecret)
    };
});

services.AddSingleton<IAmazonS3>(x =>
{
    var options = x.GetRequiredService<AWSOptions>();
    var config = new AmazonS3Config
    {
        ServiceURL = "https://s3.yandexcloud.net"
    };
    
    return new AmazonS3Client(options.Credentials, config);
});

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

await app.RunAsync();
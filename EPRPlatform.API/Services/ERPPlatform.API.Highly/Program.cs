using Autofac.Extensions.DependencyInjection;
using Autofac;
using EPRPlatform.API.Interfaces;
using EPRPlatform.API.Repository;
using EPRPlatform.API.Repository.Highly;
using EPRPlatform.API.Interfaces.Highly;
using EPRPlatform.API.Extend;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//×¢ÈëLog4Net
builder.Services.AddLogging(cfg =>
{
    cfg.AddLog4Net();
});

// EFCore
builder.Services.AddDbContext<DataContext>(option => option.UseSqlServer(configuration.GetConnectionString("Default")));

//redis
builder.Services.AddRedisInvoker(options =>
{
    options.RedisConnectionString = configuration.GetValue<string>("RedisConnectionString");
});
var ee = configuration.GetSection("MongoDB").Get<MongoDBInvokerOptions>();
builder.Services.AddMongoDBInvoker(option => {
    option.MongoDBConnectionString = configuration.GetSection("MongoDB:MongoDBConnectionString").Value;
    option.DataBase = configuration.GetSection("MongoDB:DataBase").Value;
});

builder.Services.AddRabbitMQInvoker(options =>
{
    options.Host = configuration.GetSection("RabbitMQ:Host").Value;
    options.Port = int.Parse(configuration.GetSection("RabbitMQ:Port").Value);
    options.User = configuration.GetSection("RabbitMQ:User").Value;
    options.Password = configuration.GetSection("RabbitMQ:Password").Value;
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());//Ìæ»»ÈÝÆ÷
builder.Host.ConfigureContainer<ContainerBuilder>((context, containerBuilder) =>
{
    containerBuilder.RegisterType<ErrorRepository>().As<IErrorRepository>();
    containerBuilder.RegisterType<GoodsRepository>().As<IGoodsRepository>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

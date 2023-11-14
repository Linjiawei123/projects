using Autofac.Extensions.DependencyInjection;
using Autofac;
using EPRPlatform.API.Interfaces;
using EPRPlatform.API.Repository;
using EPRPlatform.API.Repository.Highly;
using EPRPlatform.API.Interfaces.Highly;
using EPRPlatform.API.Extend;
using EPRPlatform.API.Modles;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EFCore
builder.Services.AddDbContext<DataContext>(option => option.UseSqlServer(configuration.GetConnectionString("Default")));

//redis
builder.Services.AddRedisInvoker(options =>
{
    options.RedisConnectionString = configuration.GetValue<string>("RedisConnectionString");
});

builder.Services.AddRabbitMQInvoker(options =>
{
    options.AddRange(configuration.GetSection("RabbitMQ").Get<List<RabbitMQInvokerOptions>>());
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

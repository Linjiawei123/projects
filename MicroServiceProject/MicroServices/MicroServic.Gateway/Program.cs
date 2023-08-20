using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Cache.CacheManager;
using Ocelot.Provider.Polly;
using IdentityServer4.AccessTokenValidation;
using MicroService.Dto.PublicModels;
using MicroService.Extend.Middleware;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
configuration.AddJsonFile("ocelotsettings.json", false, true);//表示读取配置文件


// Cors
builder.Services.Configure<Cors>(configuration.GetSection("Cors"));
Cors corsConfig = new();
configuration.GetSection("Cors").Bind(corsConfig);
builder.Services.AddCors(options =>
{
    options.AddPolicy("Cors",
        builder =>
        builder.SetIsOriginAllowedToAllowWildcardSubdomains()
        .WithOrigins(corsConfig.Origins)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddOcelot()
    .AddConsul()
    .AddCacheManager(x =>
    {
        x.WithDictionaryHandle();//默认字典存储---默认---换成分布式的--换个存储的介质
    })
    .AddPolly();//IOC注册，Ocelot怎么处理,consul 负载均衡，动态伸缩，CacheManager 缓存


#region Ids4
var authenticationProviderKey = "UserGatewayKey";
builder.Services.AddAuthentication("Bearer")
    .AddIdentityServerAuthentication(authenticationProviderKey, options =>
    {
        options.Authority = configuration.GetValue<string>("Authority");
        //options.ApiName = "OAApi";
        options.RequireHttpsMetadata = false;
        options.SupportedTokens = SupportedTokens.Both;
    });
#endregion


var app = builder.Build();

app.UseCors("Cors");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<XssFilterMiddleware>();//xss过滤

app.UseWebSockets();

app.UseOcelot();//使用Ocelot来完成http请求

app.Run();

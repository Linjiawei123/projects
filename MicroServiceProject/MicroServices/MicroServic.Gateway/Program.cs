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
configuration.AddJsonFile("ocelotsettings.json", false, true);//��ʾ��ȡ�����ļ�


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
        x.WithDictionaryHandle();//Ĭ���ֵ�洢---Ĭ��---���ɷֲ�ʽ��--�����洢�Ľ���
    })
    .AddPolly();//IOCע�ᣬOcelot��ô����,consul ���ؾ��⣬��̬������CacheManager ����


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

app.UseMiddleware<XssFilterMiddleware>();//xss����

app.UseWebSockets();

app.UseOcelot();//ʹ��Ocelot�����http����

app.Run();

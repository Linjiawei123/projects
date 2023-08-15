using MicroService.AuthenticationCenter;
using MicroService.Repository;
using MicroService.Interfaces;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.Services;
using IdentityServer4.Validation;

//顶级语句
var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EFCore
builder.Services.AddDbContext<DataContext>(option => option.UseSqlServer(configuration.GetConnectionString("Default")));


#region IOC注册
// 添加IdentityServer服务
builder.Services.AddIdentityServer()
    .AddInMemoryIdentityResources(Config.GetIdentityResources())
    .AddInMemoryApiScopes(Config.GetApiScopes())
    .AddInMemoryClients(Config.GetClients())
    .AddDeveloperSigningCredential();
#endregion
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddScoped<IResourceOwnerPasswordValidator, CustomResourceOwnerPasswordValidator>();
builder.Services.AddTransient<IProfileService, CProfileService>();//自定义Claim


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region 中间件
app.UseIdentityServer();//使用中间件来处理请求
#endregion

app.Run();

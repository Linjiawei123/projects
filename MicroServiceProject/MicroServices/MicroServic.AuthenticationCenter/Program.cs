using MicroService.AuthenticationCenter;
using MicroService.Repository;
using MicroService.Interfaces;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.Services;
using IdentityServer4.Validation;

//�������
var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EFCore
builder.Services.AddDbContext<DataContext>(option => option.UseSqlServer(configuration.GetConnectionString("Default")));


#region IOCע��
// ���IdentityServer����
builder.Services.AddIdentityServer()
    .AddInMemoryIdentityResources(Config.GetIdentityResources())
    .AddInMemoryApiScopes(Config.GetApiScopes())
    .AddInMemoryClients(Config.GetClients())
    .AddDeveloperSigningCredential();
#endregion
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddScoped<IResourceOwnerPasswordValidator, CustomResourceOwnerPasswordValidator>();
builder.Services.AddTransient<IProfileService, CProfileService>();//�Զ���Claim


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region �м��
app.UseIdentityServer();//ʹ���м������������
#endregion

app.Run();

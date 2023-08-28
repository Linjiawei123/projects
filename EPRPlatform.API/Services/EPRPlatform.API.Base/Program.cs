using Autofac.Extensions.DependencyInjection;
using Autofac;
using EPRPlatform.API.Dto.PublicModels;
using EPRPlatform.API.Interfaces;
using EPRPlatform.API.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Cors
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

// EFCore
builder.Services.AddDbContext<DataContext>(option => option.UseSqlServer(configuration.GetConnectionString("Default")));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
// Swagger
builder.Services.AddSwaggerGen(options =>
{
    // ע��
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // �ڶ�������Ϊ�Ƿ���ʾ������ע��,����ѡ��true
    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), true);
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "����ƽ̨API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = "ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�",
        Name = "Authorization",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },Array.Empty<string>()
        }
    });
});


builder.Services.AddAuthentication("Bearer")
.AddIdentityServerAuthentication(options =>
{
    options.Authority = configuration.GetValue<string>("Authority");
    options.RequireHttpsMetadata = false;

});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());//�滻����
builder.Host.ConfigureContainer<ContainerBuilder>((context, containerBuilder) =>
{
    containerBuilder.RegisterType<ErrorRepository>().As<IErrorRepository>();
    containerBuilder.RegisterType<BSInvenTypeRepository>().As<IBSInvenTypeRepository>();
    containerBuilder.RegisterType<BSDepartmentRepository>().As<IBSDepartmentRepository>();
    containerBuilder.RegisterType<BSCostTypeRepository>().As<IBSCostTypeRepository>();
    containerBuilder.RegisterType<BSInvenRepository>().As<IBSInvenRepository>();
    containerBuilder.RegisterType<BSSupplierRepository>().As<IBSSupplierRepository>();
    containerBuilder.RegisterType<BSCustomerRepository>().As<IBSCustomerRepository>();
    containerBuilder.RegisterType<BSEmployeeRepository>().As<IBSEmployeeRepository>();
});


var app = builder.Build();

app.UseCors("Cors");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger(c =>
{
    c.PreSerializeFilters.Add((doc, _) =>
    {
        doc.Servers?.Clear();
    });
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "EPRPlatform.API"); c.RoutePrefix = "swagger";
    c.DocExpansion(DocExpansion.None);
    c.DefaultModelsExpandDepth(-1);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

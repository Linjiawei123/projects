using Microsoft.EntityFrameworkCore;
using EPRPlatform.API.Repository;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EPRPlatform.API.File;
using EPRPlatform.API.File.Model;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using EPRPlatform.API.Method;
using System.Reflection;
using Autofac.Core;
using Microsoft.AspNetCore.Http.Features;
using EPRPlatform.API.Extend;
using EPRPlatform.API.Dto.PublicModels;
using System.Net;
using Microsoft.Extensions.FileProviders;
using System.IO;
using EPRPlatform.API.Interfaces;
using EPRPlatform.API.Dto.OAModels;

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
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), true);
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "�ļ�����API", Version = "v1" });
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
            },new string[] {}
        }
    });
});

builder.Services.AddAuthentication("Bearer")
.AddIdentityServerAuthentication(options =>
{
    options.Authority = configuration.GetValue<string>("Authority");
    //options.ApiName = "OAApi";
    options.RequireHttpsMetadata = false;

});

//���������ֵ
builder.WebHost.ConfigureKestrel((context, options) =>
{
    //�������1G, ����ĵ�λ��byte
    options.Limits.MaxRequestBodySize = 1073741824;
});

//�����ֵ
builder.Services.Configure<FormOptions>(option =>
{
    //�������1G, ����ĵ�λ��byte
    option.MultipartBodyLengthLimit = 1073741824;
});

UploadSetting uploadConfig = new();
configuration.GetSection("UploadModels").Bind(uploadConfig);
builder.Services.AddOptions().Configure<UploadSetting>(configuration.GetSection("UploadModels"));


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>((context, containerBuilder) =>
{
    containerBuilder.RegisterType<ErrorRepository>().As<IErrorRepository>();
    containerBuilder.RegisterType<UploadRepository>().As<IUploadRepository>();
    containerBuilder.RegisterType<UserRepository>().As<IUserRepository>();
    containerBuilder.RegisterType<LoginUserRepository>().As<ILoginUserRepository>();
});


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
BaseHttpContext.ServiceCollection = builder.Services;


// Http�м��
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("Cors");

app.UseHttpsRedirection();

app.UseAuthentication();//��֤
app.UseAuthorization();//��Ȩ

app.UseMiddleware<UserInfoMiddleware>();

app.MapControllers();


app.UseStaticFiles();
app.UseFileServer(new FileServerOptions
{

    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), uploadConfig.FileProvider)),
    RequestPath = new PathString(uploadConfig.RequestPath),
    EnableDirectoryBrowsing = true
});

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

//consul�������
app.MapWhen(context => context.Request.Path.Equals("/Api/Health/Index"),
    applicationBuilder => applicationBuilder.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.OK;
        await context.Response.WriteAsync("Ok");
    }));

app.Configuration.ConsulRegist();

app.Run();

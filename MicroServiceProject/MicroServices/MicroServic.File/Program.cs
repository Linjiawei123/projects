using Microsoft.EntityFrameworkCore;
using MicroService.Repository;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MicroService.File;
using MicroService.File.Model;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using MicroService.Method;
using System.Reflection;
using Autofac.Core;
using Microsoft.AspNetCore.Http.Features;
using MicroService.Extend;
using MicroService.Dto.PublicModels;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;





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

// EFCore
builder.Services.AddDbContext<DataContext>(option => option.UseSqlServer(configuration.GetConnectionString("Default")));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();



// Swagger
builder.Services.AddSwaggerGen(options =>
{
    // 注释
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // 第二个参数为是否显示控制器注释,我们选择true
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), true);
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "文件处理API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = "直接在下框中输入Bearer {token}（注意两者之间是一个空格）",
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


//读取配置文件
builder.Services.AddOptions().Configure<UploadSetting>(configuration.GetSection("UploadModels"));



builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
BaseHttpContext.ServiceCollection = builder.Services;

//初始化redis
RedisHepler.InitConnect(configuration);

//请求体最大值
builder.WebHost.ConfigureKestrel((context, options) =>
{
    //设置最大1G, 这里的单位是byte
    options.Limits.MaxRequestBodySize = 1073741824;
});

//表单最大值
builder.Services.Configure<FormOptions>(option =>
{
    //设置最大1G, 这里的单位是byte
    option.MultipartBodyLengthLimit = 1073741824;
});


// 配置容器
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacModuleRegister());
    builder.RegisterType(typeof(DataContext)).Named<DbContext>("DataContext");
    builder.RegisterAssemblyTypes(Assembly.Load("MicroService.Repository")).Where(x => x.Name.EndsWith("Repository")).WithParameter(ResolvedParameter.ForNamed<DbContext>("DataContext")).AsImplementedInterfaces();
});



// Http中间件
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseCors("Cors");
app.UseHttpsRedirection();

app.UseAuthentication();//认证
app.UseAuthorization();//授权

app.UseMiddleware<UserInfoMiddleware>();

app.MapControllers();


app.UseSwagger(c =>
{
    c.PreSerializeFilters.Add((doc, _) =>
    {
        doc.Servers?.Clear();
    });
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "MicroService"); c.RoutePrefix = "swagger";
    c.DocExpansion(DocExpansion.None);
    c.DefaultModelsExpandDepth(-1);
});

//consul健康检查
app.MapWhen(context => context.Request.Path.Equals("/Api/Health/Index"),
    applicationBuilder => applicationBuilder.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.OK;
        await context.Response.WriteAsync("Ok");
    }));

app.Configuration.ConsulRegist();

app.Run();

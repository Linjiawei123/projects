using Microsoft.EntityFrameworkCore;
using MicroService.Repository;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MicroService.OA;
using MicroService.Dto.OAModels;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using MicroService.OA.Job;
using Microsoft.Extensions.FileProviders;
using MicroService.Extend;
using MicroService.Dto.PublicModels;
using MicroService.Modles;

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


builder.Services.AddControllers(options =>
{
    //options.Filters.Add(new LogActionFilter());
});
builder.Services.AddEndpointsApiExplorer();



// Swagger
builder.Services.AddSwaggerGen(options =>
{
    // ע��
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // �ڶ�������Ϊ�Ƿ���ʾ������ע��,����ѡ��true
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), true);
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
    //options.ApiName = "OAApi";
    options.RequireHttpsMetadata = false;

});

//HttpContext
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
BaseHttpContext.ServiceCollection = builder.Services;


//redis
builder.Services.AddRedisInvoker(options =>
{
    options.RedisConnectionString = configuration.GetValue<string>("RedisConnectionString");
});
builder.Services.AddHttpInvoker(options =>
{
    
});


//��ʱ����
builder.Services.AddTransient<IHostedService, OperateLogTimeJob>();

//��ȡ�����ļ�
builder.Services.AddOptions().Configure<List<MqConfigInfo>>(configuration.GetSection("RabbitMQ"));
builder.Services.AddOptions().Configure<LoginConfig>(configuration.GetSection("LoginConfig"));





// ��������
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new AutofacModuleRegister());
});



var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//��̬�ļ�·��
IConfigurationSection section = configuration.GetSection("StaticFilePath");
List<StaticFilePath> paths = section.Get<List<StaticFilePath>>();
if (paths != null && paths.Count > 0)
{
    foreach (StaticFilePath path in paths)
    {
        if (Directory.Exists(path.FileProvider))
        {
            PhysicalFileProvider fileProvider = new(path.FileProvider);
            DefaultFilesOptions defoptions = new();
            defoptions.DefaultFileNames.Clear();
            defoptions.FileProvider = fileProvider;
            defoptions.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(defoptions);

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(path.FileProvider),
                RequestPath = new PathString(path.RequestPath)
            });
        }
    }
}


app.UseCors("Cors");

app.UseHttpsRedirection();

app.UseAuthentication();//��֤

app.UseAuthorization();//��Ȩ

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

app.Use((context, next) =>
{
    context.Request.EnableBuffering();
    return next();
});
app.Run();

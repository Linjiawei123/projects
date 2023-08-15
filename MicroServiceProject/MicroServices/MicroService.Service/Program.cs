using MicroService.Method;
using MicroService.Repository;
using MicroService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;
using MicroService.Extend;
using MicroService.Extend.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


//builder.Services.AddAuthentication("Bearer")
//.AddIdentityServerAuthentication(options =>
//{
//    options.Authority = configuration.GetValue<string>("Authority");
//    options.ApiName = "OAApi";
//    options.RequireHttpsMetadata = false;

//});

builder.Services.AddTransient<ILoginUserRepository, LoginUserRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 启用请求正文缓冲
app.Use((context, next) =>
{
    context.Request.EnableBuffering(); // 启用请求正文缓冲
    return next();
});


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<UserInfoMiddleware>();

app.MapControllers();


////consul健康检查
//app.MapWhen(context => context.Request.Path.Equals("/Api/Health/Index"),
//    applicationBuilder => applicationBuilder.Run(async context =>
//    {
//        context.Response.StatusCode = (int)HttpStatusCode.OK;
//        await context.Response.WriteAsync("Ok");
//    }));

//app.Configuration.ConsulRegist();

app.Run();

using Autofac.Extensions.DependencyInjection;
using Autofac;
using MicroService.Dto.PublicModels;
using MicroService.Extend;
using MicroService.Interfaces;
using MicroService.Repository;
using MicroService.SignalR;
using MicroService.SignalR.Hubs;
using Microsoft.EntityFrameworkCore;
using MicroService.Models;
using AutoMapper;
using MicroService.Dto.SinalRModels;

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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(option => option.UseSqlServer(configuration.GetConnectionString("Default")));

builder.Services.AddAuthentication("Bearer")
.AddIdentityServerAuthentication(options =>
{
    options.Authority = configuration.GetValue<string>("Authority");
    //options.ApiName = "OAApi";
    options.RequireHttpsMetadata = false;

});

builder.Services.AddSignalR();

builder.Services.AddRedisInvoker(options =>
{
    options.RedisConnectionString = configuration.GetValue<string>("RedisConnectionString");
});


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());//Ìæ»»ÈÝÆ÷
builder.Host.ConfigureContainer<ContainerBuilder>((context, containerBuilder) =>
{
    containerBuilder.RegisterType<ErrorRepository>().As<IErrorRepository>();
    containerBuilder.RegisterType<ChatMessageRepository>().As<IChatMessageRepository>();
    containerBuilder.RegisterType<ChatMessageTimeJob>().As<IHostedService>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Cors");

app.UseHttpsRedirection();

//ÅäÖÃ
app.MapHub<ChatHub>("/chatHub");

app.MapGet("/api/GetUsers", async (string UserId, IChatMessageRepository ChatMessageRepository) =>
{
    try
    {
        List<User> userList = await ChatMessageRepository.GetUsersAsync(Guid.Parse(UserId));
        List<string> uids = userList.Select(w => w.Id.ToString()).ToList();
        List<ChatMessage> chatMessages = await ChatMessageRepository.GetChatMessagesAsync(uids, 50);
        List<ChatUsers> chatUsers = new();
        foreach (var user in userList)
        {
            ChatUsers data = new()
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Avatar = user.Url ?? "",
                messages = new List<ChatMessageSimple>()
            };
            data.messages = chatMessages.Where(w => w.SenderId == data.Id || w.ReceiverId == data.Id)
            .Select(w => new ChatMessageSimple 
            { 
                Id = w.Id.ToString(), 
                SenderId = w.SenderId, 
                ReceiverId = w.ReceiverId, 
                Content = w.Content, 
                Type = w.Type,
                SendTime = w.SendTime 
            }).OrderBy(w => w.SendTime).ToList();
            chatUsers.Add(data);
        }
        return chatUsers;
    }
    catch (Exception ex)
    {
        throw new Exception(ex.Message);
    }
})
.WithName("GetUsers");

app.MapGet("/api/ChatRecords", async (string SenderId, string ReceiverId, int RecordSize, IChatMessageRepository ChatMessageRepository) =>
{
    return await ChatMessageRepository.ChatRecordsAsync(SenderId, ReceiverId, RecordSize);
})
.WithName("ChatRecords");

app.Run();

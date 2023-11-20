using EPRPlatform.API.Interfaces;
using EPRPlatform.API.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace EPRPlatform.API.Extend
{
    public static class RabbitMQInvokerExtension
    {
        public static void AddRabbitMQInvoker(this IServiceCollection services, Action<List<RabbitMQInvokerOptions>> action)
        {
            services.Configure<List<RabbitMQInvokerOptions>>(action);
            services.AddTransient<IErrorRepository, ErrorRepository>();
            services.AddTransient<IRabbitMQInvoker, RabbitMQInvoker>();
        }

        public static void AddRabbitMQInvoker(this IServiceCollection services)
        {
            services.AddRabbitMQInvoker(_ => { });
        }
    }
}

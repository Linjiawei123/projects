using EPRPlatform.API.Interfaces;
using EPRPlatform.API.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace EPRPlatform.API.Extend
{
    public static class RabbitMQInvokerExtension
    {
        public static void AddRabbitMQInvoker(this IServiceCollection services, Action<RabbitMQInvokerOptions> action)
        {
            services.Configure<RabbitMQInvokerOptions>(action);
            services.AddTransient<IRabbitMQInvoker, RabbitMQInvoker>();
        }

        public static void AddRabbitMQInvoker(this IServiceCollection services)
        {
            services.AddRabbitMQInvoker(_ => { });
        }
    }
}

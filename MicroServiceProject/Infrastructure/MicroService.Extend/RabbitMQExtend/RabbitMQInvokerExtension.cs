using MicroService.Interfaces;
using MicroService.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace MicroService.Extend
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
#pragma warning disable CS8625 // 无法将 null 字面量转换为非 null 的引用类型。
            services.AddRabbitMQInvoker(null);
#pragma warning restore CS8625 // 无法将 null 字面量转换为非 null 的引用类型。
        }
    }
}

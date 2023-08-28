
using Microsoft.Extensions.DependencyInjection;

namespace EPRPlatform.API.Extend
{
    /// <summary>
    /// IOC注册
    /// </summary>
    public static class RedisInvokerExtension
    {
        public static void AddRedisInvoker(this IServiceCollection services, Action<RedisInvokerOptions> action)
        {
            services.Configure<RedisInvokerOptions>(action);
            services.AddTransient<IRedisInvoker, RedisInvoker>();
        }

        public static void AddRedisInvoker(this IServiceCollection services)
        {
#pragma warning disable CS8625 // 无法将 null 字面量转换为非 null 的引用类型。
            services.AddRedisInvoker(null);
#pragma warning restore CS8625 // 无法将 null 字面量转换为非 null 的引用类型。
        }
    }
}

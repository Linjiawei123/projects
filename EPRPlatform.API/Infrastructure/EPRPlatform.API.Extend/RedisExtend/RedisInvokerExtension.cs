
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
            services.AddRedisInvoker(_ => { });
        }
    }
}

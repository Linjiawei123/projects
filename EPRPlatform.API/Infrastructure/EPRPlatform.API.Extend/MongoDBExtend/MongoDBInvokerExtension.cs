using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Extend.MongoDBExtend
{
    public static class MongoDBInvokerExtension
    {
        public static void AddMongoDBInvoker(this IServiceCollection services, Action<MongoDBInvokerOptions> action)
        {
            services.Configure<MongoDBInvokerOptions>(action);
            services.AddTransient<IMongoDBInvoker, MongoDBInvoker>();
        }

        public static void AddRedisInvoker(this IServiceCollection services)
        {
#pragma warning disable CS8625 // 无法将 null 字面量转换为非 null 的引用类型。
            services.AddMongoDBInvoker(null);
#pragma warning restore CS8625 // 无法将 null 字面量转换为非 null 的引用类型。
        }
    }
}

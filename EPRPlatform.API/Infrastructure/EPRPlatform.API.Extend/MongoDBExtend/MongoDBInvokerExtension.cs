using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Extend
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
            services.AddMongoDBInvoker(_ => { });
        }
    }
}

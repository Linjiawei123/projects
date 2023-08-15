using Autofac.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace MicroService.File
{
    /// <summary>
    /// 数据库上下文注册
    /// </summary>
    public static class HostBuilderExtend
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="builder"></param>
        public static void Register(this WebApplicationBuilder builder)
        {

            #region 配置数据库
            List<Assembly> assemblyList = new()
            {
                Assembly.Load("上下文程序集")
            };
            builder.Services.AddAllDbContexts(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
                //将SQL语句输入到控制台
                options.LogTo(Console.WriteLine);
            }, assemblyList);
            #endregion
        }

        /// <summary>
        /// 注册所有数据库上下文
        /// </summary>
        /// <param name="services">服务容器</param>
        /// <param name="options">数据库上下文配置</param>
        /// <param name="assemblies">程序集</param>
        /// <returns></returns>
        public static void AddAllDbContexts(this IServiceCollection services,
            Action<DbContextOptionsBuilder> options,
             List<Assembly> assemblies)
        {
            //AddDbContext方法的参数类型
            Type[] types = new Type[] {typeof(IServiceCollection),typeof(Action<DbContextOptionsBuilder>),typeof(ServiceLifetime),typeof(ServiceLifetime)};
            //获得AddDbContext方法
            var methodAddDbContext = typeof(EntityFrameworkServiceCollectionExtensions).GetMethod("AddDbContext", 1, types);
            foreach (var asmToLoad in assemblies)
            {
                //获取所有继承自DbContext的类
                foreach (var dbCtxType in asmToLoad.GetTypes().Where(m => !m.IsAbstract && typeof(DbContext).IsAssignableFrom(m)))
                {
                    var methodGenericAddDbContext = methodAddDbContext.MakeGenericMethod(dbCtxType);
                    methodGenericAddDbContext.Invoke(null, new object[]
                    {
                        services,options,ServiceLifetime.Scoped,ServiceLifetime.Scoped,
                    });
                }
            }
        }
    }
}

using Autofac;
using Autofac.Core;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MicroService.Repository;
using MicroService.Extend;

namespace MicroService.OA
{
    /// <summary>
    /// Autofac
    /// </summary>
    public class AutofacModuleRegister:Autofac.Module
    {
        /// <summary>
        /// 重写Autofac管道Load方法，在这里注册注入
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            string BASE_REPOSITORY = "MicroService.Repository";
            builder.RegisterType(typeof(DataContext)).Named<DbContext>("DataContext");
            builder.RegisterType(typeof(OperateLogAttribute)).PropertiesAutowired();
            builder.RegisterAssemblyTypes(Assembly.Load(BASE_REPOSITORY))
            .Where(t => t.Name.EndsWith("Repository"))
            .WithParameter(ResolvedParameter.ForNamed<DbContext>("DataContext"))
            .AsImplementedInterfaces();
        }
    }
}

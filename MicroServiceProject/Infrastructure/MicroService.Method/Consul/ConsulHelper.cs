using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MicroService.Method
{
    /// <summary>
    /// Consul服务注册发现
    /// </summary>
    public static class ConsulHelper
    {
        /// <summary>
        /// Consul服务注册
        /// </summary>
        /// <param name="configuration"></param>
        public static void ConsulRegist(this IConfiguration configuration/*, IHostApplicationLifetime applicationLifetime*/)
        {
            ConsulClient client = new(c =>
            {
                c.Address = new Uri("http://localhost:8500/");
                c.Datacenter = "dc1";
            });
            string ip = string.IsNullOrWhiteSpace(configuration["ip"]) ? "127.0.0.1" : configuration["ip"];
            //string ip = "127.0.0.1";
            int port = int.Parse(configuration["port"]);//命令行参数必须传入
            int weight = string.IsNullOrWhiteSpace(configuration["weight"]) ? 1 : int.Parse(configuration["weight"]);
            string serviceID = "service" + Guid.NewGuid();
            client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = serviceID,//服务编号，不能重复，用Guid 最简单 
                Name = "MicroService",//服务名
                Address = ip,//服务提供者的能被消费者访问的ip
                Port = port,// 服务提供者的能被消费者访问的端口 
                Tags = new string[] { weight.ToString() },//标签，可以配置一下额外的参数用于传递
                Check = new AgentServiceCheck()
                {
                    Interval = TimeSpan.FromSeconds(12),//健康检查时间间隔，或者称为心跳 间隔
                    HTTP = $"http://{ip}:{port}/Api/Health/Index",//健康检查地址 
                    Timeout = TimeSpan.FromSeconds(5),//检测等待时间
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(120)//服务停止多久后注销
                }
            }).Wait();//Consult 客户端的所有方法几乎都是异步方法，但是都没按照规范加上       Async 后缀，所以容易误导。记得调用后要Wait()或者 await
                      //命令行参数获取
            Console.WriteLine($"注册成功：{ip}:{port}--weight:{weight}");

            ////程序正常退出的时候从Consul 注销服务 ,要通过方法参数注入 IHostApplicationLifetime 
            //applicationLifetime.ApplicationStopped.Register(() =>
            //{
            //    Console.WriteLine("程序正常退出的时候从Consul 注销服务 ");
            //    client.Agent.ServiceDeregister(serviceID).Wait();
            //});
        }


    }
}

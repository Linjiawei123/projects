
namespace MicroService.Extend
{
    public class RabbitMQInvokerOptions
    {
        /// <summary>
        /// MQ安装的实际服务器IP地址
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 服务端口号
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 设定一个Exchange名称
        /// </summary>
        public string ExchangeName { get; set; }
        /// <summary>
        /// 是否启用持久化
        /// </summary>
        public bool Durable { get; set; }
    }
}

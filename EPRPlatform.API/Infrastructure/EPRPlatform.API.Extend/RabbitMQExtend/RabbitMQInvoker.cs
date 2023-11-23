using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Options;

namespace EPRPlatform.API.Extend
{
    public class RabbitMQInvoker : IRabbitMQInvoker, IDisposable
    {
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQInvoker(IOptions<RabbitMQInvokerOptions> option)
        {
            _factory = new ConnectionFactory
            {
                HostName = option.Value.Host,
                Port = option.Value.Port,
                UserName = option.Value.User,
                Password = option.Value.Password
            };

            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
        }
        /// <summary>
        /// 声明Exchange
        /// </summary>
        /// <param name="exchangeName">交换机名称</param>
        /// <param name="exchangeType">交换机类型</param>
        public void DeclareExchange(string exchangeName, string exchangeType)
        {
            _channel.ExchangeDeclare(exchangeName, exchangeType);
        }
        /// <summary>
        /// 声明队列
        /// </summary>
        /// <param name="queueName">队列名称</param>
        /// <param name="durable">是否持久化，设置为true表示持久化。</param>
        /// <param name="exclusive">是否排他性，设置为true表示只能被一个连接（connection）使用，用于创建临时队列。</param>
        /// <param name="autoDelete">是否自动删除，设置为true表示连接断开时自动删除队列。</param>
        public void DeclareQueue(string queueName, bool durable, bool exclusive, bool autoDelete)
        {
            _channel.QueueDeclare(queueName, durable, exclusive, autoDelete);
        }
        /// <summary>
        /// 将队列绑定到交换机
        /// </summary>
        /// <param name="queueName">队列名称</param>
        /// <param name="exchangeName">交换机名称</param>
        /// <param name="routingKey">路由键</param>
        public void BindQueueToExchange(string queueName, string exchangeName, string routingKey)
        {
            _channel.QueueBind(queueName, exchangeName, routingKey);
        }
        /// <summary>
        /// 发布消息到交换机
        /// </summary>
        /// <param name="exchangeName">交换机名称</param>
        /// <param name="routingKey">路由键</param>
        /// <param name="message">消息内容</param>
        public void PublishMessage(string exchangeName, string routingKey, string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchangeName, routingKey, null, body);
        }
        /// <summary>
        /// 启动消息消费者
        /// </summary>
        /// <param name="queueName">队列名称</param>
        /// <param name="messageHandler">消息处理的回调方法</param>
        public void StartConsumer(string queueName, Action<string> messageHandler)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var receivedMessage = Encoding.UTF8.GetString(ea.Body.ToArray());
                messageHandler(receivedMessage);
            };

            _channel.BasicConsume(queueName, true, consumer);
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}

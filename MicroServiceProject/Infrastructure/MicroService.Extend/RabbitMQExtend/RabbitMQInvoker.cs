using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using MicroService.Interfaces;

namespace MicroService.Extend
{
    public class RabbitMQInvoker : IRabbitMQInvoker
    {
        private readonly IErrorRepository _iError;
        private readonly List<RabbitMQInvokerOptions> _rabbitMQOptions;
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;
        /// <summary>
        /// 构造函数
        /// </summary>
        public RabbitMQInvoker(IErrorRepository iError, IOptions<List<RabbitMQInvokerOptions>> options)
        {
            _iError = iError;
            _rabbitMQOptions = options.Value;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="message"></param>
        public void SendMsg(string userStr, string queueName, string message)
        {
            try
            {
                InitConnection(userStr);

                // 声明队列
                _channel.QueueDeclare(queue: queueName,
                    true,// 设置为true表示队列是持久化的
                    false, false, null);

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                _channel.BasicPublish("", queueName, null, body);

            }
            catch (Exception ex)
            {
                _iError.AddErrorAsync(ex).Wait();
            }
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="messageHandler"></param>
        public string Receive(string userStr, string queueName, Action<string> messageHandler)
        {
            string message = "";
            try
            {
                InitConnection(userStr);

                // 声明队列（接收需声明队列，否则队列不存在时，无法接收消息）
                _channel.QueueDeclare(queueName,
                    true, // 设置为true表示队列是持久化的
                    false, false, null);

                //设置消费者数量（并发度）,每个消费者每次只能处理一条消息
                _channel.BasicQos(0, 1, false);

                // 创建消费者
                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (model, ea) =>
                {
                    try
                    {
                        message = Encoding.UTF8.GetString(ea.Body.ToArray());
                        messageHandler(message);
                        // 消息处理成功，确认消息
                        _channel.BasicAck(ea.DeliveryTag, false);
                    }
                    catch (Exception ex)
                    {
                        // 消息处理异常，确认消息
                        _channel.BasicAck(ea.DeliveryTag, false);
                        _iError.AddErrorAsync(ex).Wait();
                    }
                };

                _channel.BasicConsume(queueName,
                    false,// 设置为true表示自动确认消息
                    consumer);

            }
            catch (Exception ex)
            {
                _iError.AddErrorAsync(ex).Wait();
            }
            return message;
        }

        /// <summary>
        /// 初始化链接
        /// </summary>
        private void InitConnection(string userStr)
        {
            var option = _rabbitMQOptions.Find(w => w.User == userStr);
            // 设置连接参数
            _factory = new ConnectionFactory() { HostName = option.Host, UserName = option.User, Password = option.Password };
            if (_connection == null || !_connection.IsOpen)
            {
                _connection = _factory.CreateConnection();
                _channel = _connection.CreateModel();
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
            _connection?.Close();
            _connection?.Dispose();
        }
    }
}

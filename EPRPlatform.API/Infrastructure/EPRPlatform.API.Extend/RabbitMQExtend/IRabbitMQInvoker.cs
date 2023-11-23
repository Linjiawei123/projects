using MongoDB.Driver.Core.Bindings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace EPRPlatform.API.Extend
{
    public interface IRabbitMQInvoker
    {
        void DeclareExchange(string exchangeName, string exchangeType);

        void DeclareQueue(string queueName, bool durable, bool exclusive, bool autoDelete);

        void BindQueueToExchange(string queueName, string exchangeName, string routingKey);

        void PublishMessage(string exchangeName, string routingKey, string message);

        void StartConsumer(string queueName, Action<string> messageHandler);
    }
}

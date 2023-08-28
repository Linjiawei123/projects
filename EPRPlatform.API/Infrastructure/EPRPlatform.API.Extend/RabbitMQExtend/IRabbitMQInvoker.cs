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
        /// <summary>
        /// 发送消息
        /// </summary>
        void SendMsg(string userStr, string queName, string msg);
        /// <summary>
        /// 消费消息
        /// </summary>
        string Receive(string userStr, string queName, Action<string> messageHandler);
    }
}

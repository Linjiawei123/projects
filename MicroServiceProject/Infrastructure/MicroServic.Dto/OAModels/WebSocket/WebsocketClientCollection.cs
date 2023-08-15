
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService.OA
{
    /// <summary>
    /// 连接池
    /// </summary>
    public class WebsocketClientCollection
    {
        private static List<WebsocketClient> _clients = new List<WebsocketClient>();
        /// <summary>
        /// 添加连接
        /// </summary>
        /// <param name="client"></param>
        public static void Add(WebsocketClient client)
        {
            _clients.Add(client);
        }
        /// <summary>
        /// 删除连接
        /// </summary>
        /// <param name="client"></param>
        public static void Remove(WebsocketClient client)
        {
            _clients.Remove(client);
        }
        /// <summary>
        /// 获取连接
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static WebsocketClient Get(int UserId)
        {
            var client = _clients.FirstOrDefault(c => c.UserId == UserId);

            return client;
        }
        /// <summary>
        /// 获取房间号连接
        /// </summary>
        /// <param name="UserIds"></param>
        /// <returns></returns>
        public static List<WebsocketClient> GetGroupUser(List<int> UserIds)
        {
            var client = _clients.Where(c => UserIds.Contains(c.UserId));
            return client.ToList();

        }
    }
}

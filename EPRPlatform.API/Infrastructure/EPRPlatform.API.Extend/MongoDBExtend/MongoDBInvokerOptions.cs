using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Extend
{
    public class MongoDBInvokerOptions
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string MongoDBConnectionString { get; set; } = string.Empty;
        /// <summary>
        /// 数据库
        /// </summary>
        public string DataBase { get; set; } = string.Empty;
    }
}

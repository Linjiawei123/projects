using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPRPlatform.API.Host
{
    /// <summary>
    /// 发送信息体
    /// </summary>
    public class Message
    {
        /// <summary>
        /// 发送者id
        /// </summary>
        public int FromUserId { get; set; }
        /// <summary>
        /// 接受者id
        /// </summary>
        public int? ToUserId { get; set; }
        /// <summary>
        /// 群组Id
        /// </summary>
        public int? ToGroupId { get; set; }
        /// <summary>
        /// 发了方法
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 信息体
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string Nick { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }
    }
}

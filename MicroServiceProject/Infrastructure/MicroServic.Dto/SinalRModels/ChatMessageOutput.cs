
namespace MicroService.Dto.SinalRModels
{
    public class ChatUsers
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        public List<ChatMessageSimple> messages { get; set; }

    }
    public class ChatMessageSimple
    {
        /// <summary>
        /// 数据id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 发送者id
        /// </summary>
        public string SenderId { get; set; }
        /// <summary>
        /// 接收者id
        /// </summary>
        public string ReceiverId { get; set; }
        /// <summary>
        /// 信息内容
        /// </summary>
        public string Content { get; set; } = "";
        /// <summary>
        /// 信息类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public string SendTime { get; set; }
    }
}

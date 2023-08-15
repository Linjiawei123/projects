namespace MicroService.Dto.OAModels
{
    /// <summary>
    /// 用户输出模型
    /// </summary>
    public class UserSimple
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 用户类型 1.超级管理员 2.管理员 3.普通用户
        /// </summary>
        public int UserType { get; set; }
        /// <summary>
        /// 账号状态 1.正常 2.禁用 3.锁定
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 权限id
        /// </summary>
        public List<Guid> Rights { get; set; }
    }
    /// <summary>
    /// 用户模型
    /// </summary>
    public class UserData
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Name { get; set; }
    }
}

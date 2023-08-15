namespace MicroService.Models
{
    /// <summary>
    /// 用户拥有的权限
    /// </summary>
    public class UserRights
    {
        /// <summary>
        /// 权限Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 权限代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 菜单Id
        /// </summary>
        public Guid MenuId { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Menu { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }
    }
}


namespace MicroService.Models
{
    /// <summary>
    /// 菜单权限
    /// </summary>
    public class MenuRights 
    {
        /// <summary>
        /// 菜单id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 父级id
        /// </summary>
        public Guid ParentId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 菜单代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 菜单权限
        /// </summary>
        public List<Permission> Rights { get; set; }
    }
}


using MicroService.Models;

namespace MicroService.Dto.OAModels
{
    /// <summary>
    /// 菜单输出
    /// </summary>
    public class MenuSimple
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
        /// 跳转地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public int Grade { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// 是否打开新页面
        /// </summary>
        public bool IsBlank { get; set; }
        /// <summary>
        /// 子菜单
        /// </summary>
        public List<MenuSimple> Child { get; set; }
    }
    /// <summary>
    /// 菜单输出
    /// </summary>
    public class MenuRightsSimple
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
        public List<SelectRights> Rights { get; set; }
        /// <summary>
        /// 子菜单
        /// </summary>
        public List<MenuRightsSimple> Child { get; set; }
    }
    /// <summary>
    /// 选择状态的权限
    /// </summary>
    public class SelectRights : Permission
    {
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelect { get; set; }
    }
}

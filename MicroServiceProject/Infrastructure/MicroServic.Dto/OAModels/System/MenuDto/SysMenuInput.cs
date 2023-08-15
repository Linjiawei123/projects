
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MicroService.Dto.OAModels
{
    /// <summary>
    /// 新增
    /// </summary>
    public class SysMenuAdd
    {
        /// <summary>
        /// 父级id
        /// </summary>
        [DisplayName("父级id")]
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0}长度超过{1}位或输入了空字符串")]
        public string Name { get; set; }
        /// <summary>
        /// 菜单代码
        /// </summary>
        [DisplayName("菜单代码")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0}长度超过{1}位或输入了空字符串")]
        public string Code { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        [Column("Sort", TypeName = "int")]
        [Required(ErrorMessage = "请输入{0}")]
        public int Sort { get; set; }
        /// <summary>
        /// 跳转地址
        /// </summary>
        [DisplayName("跳转地址")]
        [StringLength(50, ErrorMessage = "{0}长度超过{1}位")]
        public string Url { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [DisplayName("图标")]
        [StringLength(20, ErrorMessage = "{0}长度超过{1}位")]
        public string Icon { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(50, ErrorMessage = "{0}长度超过{1}位")]
        public string Remark { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>
        [DisplayName("是否默认")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public bool IsDefault { get; set; }
        /// <summary>
        /// 是否打开新页面
        /// </summary>
        [DisplayName("是否打开新页面")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public bool IsBlank { get; set; }
    }
    /// <summary>
    /// 修改
    /// </summary>
    public class SysMenuUpdate
    {
        /// <summary>
        /// 菜单Id
        /// </summary>
        [DisplayName("菜单Id")]
        public Guid Id { get; set; }
        /// <summary>
        /// 父级id
        /// </summary>
        [DisplayName("父级id")]
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0}长度超过{1}位或输入了空字符串")]
        public string Name { get; set; }
        /// <summary>
        /// 菜单代码
        /// </summary>
        [DisplayName("菜单代码")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0}长度超过{1}位或输入了空字符串")]
        public string Code { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        [Column("Sort", TypeName = "int")]
        [Required(ErrorMessage = "请输入{0}")]
        public int Sort { get; set; }
        /// <summary>
        /// 跳转地址
        /// </summary>
        [DisplayName("跳转地址")]
        [StringLength(50, ErrorMessage = "{0}长度超过{1}位")]
        public string Url { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [DisplayName("图标")]
        [StringLength(20, ErrorMessage = "{0}长度超过{1}位")]
        public string Icon { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(50, ErrorMessage = "{0}长度超过{1}位")]
        public string Remark { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>
        [DisplayName("是否默认")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public bool IsDefault { get; set; }
        /// <summary>
        /// 是否打开新页面
        /// </summary>
        [DisplayName("是否打开新页面")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public bool IsBlank { get; set; }
    }
    /// <summary>
    /// 权限id
    /// </summary>
    public class RightsIds
    {
        /// <summary>
        /// 权限id
        /// </summary>
        public List<string> Ids { get; set;}
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EPRPlatform.API.Dto.OAModels
{
    /// <summary>
    /// 新增
    /// </summary>
    public class SysPermissionAdd
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        [DisplayName("菜单ID")]
        [Required(ErrorMessage = "请输入{0}")]
        public Guid MenuId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(50, MinimumLength = 1,ErrorMessage = "{0}超长")]
        public string Name { get; set; }
        /// <summary>
        /// 权限代码
        /// </summary>
        [DisplayName("权限代码")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0}超长")]
        public string Code { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        [DisplayName("简介")]
        [StringLength(100,ErrorMessage = "{0}超长")]
        public string Remark { get; set; }
    }
    /// <summary>
    /// 修改
    /// </summary>
    public class SysPermissionUpdate
    {
        /// <summary>
        /// 权限ID
        /// </summary>
        [DisplayName("权限ID")]
        [Required(ErrorMessage = "请输入{0}")]
        public Guid Id { get; set; }
        /// <summary>
        /// 菜单ID
        /// </summary>
        [DisplayName("菜单ID")]
        [Required(ErrorMessage = "请输入{0}")]
        public Guid MenuId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0}超长")]
        public string Name { get; set; }
        /// <summary>
        /// 权限代码
        /// </summary>
        [DisplayName("权限代码")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0}超长")]
        public string Code { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        [DisplayName("简介")]
        [StringLength(100, ErrorMessage = "{0}超长")]
        public string Remark { get; set; }
    }
}

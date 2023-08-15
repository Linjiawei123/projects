using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MicroService.Dto.OAModels
{
    /// <summary>
    /// 新增
    /// </summary>
    public class RoleAdd
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(50, MinimumLength = 1,ErrorMessage ="名称长度有误")]
        public string Name { get; set; }
        /// <summary>
        /// 角色编号
        /// </summary>
        [DisplayName("角色编号")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "编号长度有误")]
        public string Code { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [DisplayName("状态")]
        [Column("Status", TypeName = "bit")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public bool Status { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        [DisplayName("简介")]
        [StringLength(100, ErrorMessage = "简介长度有误")]
        public string Remark { get; set; }
        /// <summary>
        /// 权限id
        /// </summary>
        [DisplayName("权限id")]
        public List<Guid> RightsId { get; set; }
    }
    /// <summary>
    /// 修改
    /// </summary>
    public class RoleUpdate : RoleAdd
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [DisplayName("角色ID")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid Id { get; set; }
    }
    /// <summary>
    /// 用户角色新增
    /// </summary>
    public class RoleUserManage
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [DisplayName("角色ID")]
        [Required(ErrorMessage = "请输入{0}")]
        public Guid RoleId { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public List<Guid> UserId { get; set; }
    }
}

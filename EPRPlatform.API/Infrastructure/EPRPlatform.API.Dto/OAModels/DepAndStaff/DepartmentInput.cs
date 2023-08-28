using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EPRPlatform.API.Dto.OAModels
{
    /// <summary>
    /// 新增部门
    /// </summary>
    public class DepartmentAdd
    {
        /// <summary>
        /// 上级部门id
        /// </summary>
        [DisplayName("上级部门id")]
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [DisplayName("部门名称")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public string Name { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        [DisplayName("部门编号")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public string Code { get; set; }
    }
    /// <summary>
    /// 修改
    /// </summary>
    public class DepartmentUpdate : DepartmentAdd
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [DisplayName("数据id")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid Id { get; set; }
    }
}

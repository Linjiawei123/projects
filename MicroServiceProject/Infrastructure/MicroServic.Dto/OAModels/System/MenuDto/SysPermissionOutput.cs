using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MicroService.Dto.OAModels
{
    /// <summary>
    /// 简化模型
    /// </summary>
    public class SysPermissionSimple
    {
        /// <summary>
        /// 权限ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 菜单ID
        /// </summary>
        public Guid MenuId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 权限代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Remark { get; set; }
    }
}

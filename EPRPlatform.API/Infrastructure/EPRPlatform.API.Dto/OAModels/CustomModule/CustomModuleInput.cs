using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Dto.OAModels.CustomModule
{
    /// <summary>
    /// 新增模块
    /// </summary>
    public class CustomModuleAdd
    {
        /// <summary>
        /// 菜单
        /// </summary>
        [DisplayName("菜单")]
        [Required(ErrorMessage = "请选择{0}")]
        public Guid MenuId { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        [DisplayName("模块名称")]
        [Required(ErrorMessage = "请输入{0}的值")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0}长度超过{1}位或输入了空字符串")]
        public string Name { get; set; }
    }
    /// <summary>
    /// 属性
    /// </summary>
    public class PropertyAdd
    {
        /// <summary>
        /// 模块id
        /// </summary>
        [DisplayName("模块id")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int ModuleId { get; set; }
        /// <summary>
        /// 属性标题
        /// </summary>
        [DisplayName("属性标题")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0}长度超过{1}位或输入了空字符串")]
        public string Title { get; set; }
        /// <summary>
        /// 属性值类型
        /// </summary>
        [DisplayName("属性值类型")]
        [Required(ErrorMessage = "请选择{0}")]
        public int Type { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        [Required(ErrorMessage = "请输入{0}值")]
        public int Sort { get; set; }
    }

    /// <summary>
    /// 新增模块
    /// </summary>
    public class CustomModuleUpdate
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [DisplayName("数据id")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int Id { get; set; }
        /// <summary>
        /// 菜单
        /// </summary>
        [DisplayName("菜单")]
        [Required(ErrorMessage = "请选择{0}")]
        public Guid MenuId { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        [DisplayName("模块名称")]
        [Required(ErrorMessage = "请输入{0}的值")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0}长度超过{1}位或输入了空字符串")]
        public string Name { get; set; }
    }
    /// <summary>
    /// 属性
    /// </summary>
    public class PropertyUpdate
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [DisplayName("数据id")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int Id { get; set; }
        /// <summary>
        /// 属性标题
        /// </summary>
        [DisplayName("属性标题")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0}长度超过{1}位或输入了空字符串")]
        public string Title { get; set; }
        /// <summary>
        /// 属性值类型
        /// </summary>
        [DisplayName("属性值类型")]
        [Required(ErrorMessage = "请选择{0}")]
        public int Type { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        [Required(ErrorMessage = "请输入{0}值")]
        public int Sort { get; set; }
    }
}

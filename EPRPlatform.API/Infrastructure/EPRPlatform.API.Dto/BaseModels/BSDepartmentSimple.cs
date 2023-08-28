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

namespace EPRPlatform.API.Dto.BaseModels
{
    /// <summary>
    /// 部门输出模型
    /// </summary>
    public class BSDepartmentSimple
    {
        /// <summary>
        /// 数据id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public string DepartmentCode { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 是否有关联员工
        /// </summary>
        public bool IsHasEmployee { get; set; }
    }
}

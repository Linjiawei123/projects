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
    /// 输出模型
    /// </summary>
    public class BSCostTypeSimple
    {
        /// <summary>
        /// 数据id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 费用类型编号
        /// </summary>
        public string CostTypeCode { get; set; }
        /// <summary>
        /// 费用类型名称
        /// </summary>
        public string CostTypeName { get; set; }
        /// <summary>
        /// 是否有关联数据
        /// </summary>
        public bool IsHasCost { get; set; }
    }
}

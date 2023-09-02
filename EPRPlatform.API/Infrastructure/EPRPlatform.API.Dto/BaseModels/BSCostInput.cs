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
    public class BSCostAdd
    {
        /// <summary>
        /// 费用编号
        /// </summary>
        [DisplayName("费用编号")]
        [Required(ErrorMessage = "请输入费用类型编号")]
        public string CostCode { get; set; }
        /// <summary>
        /// 费用名称
        /// </summary>
        [DisplayName("费用名称")]
        [Required(ErrorMessage = "请输入费用类型编号")]
        public string CostName { get; set; }
        /// <summary>
        /// 费用类型编号
        /// </summary>
        [DisplayName("费用类型编号")]
        [Required(ErrorMessage = "请输入费用类型编号")]
        public string CostTypeCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Remark { get; set; }
    }
    public class BSCostUpdate : BSCostAdd
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [DisplayName("数据id")]
        [Required]
        public int Id { get; set; }
    }
}

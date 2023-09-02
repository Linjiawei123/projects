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
    public class BSStoreAdd
    {
        /// <summary>
        /// 仓库编号
        /// </summary>
        [DisplayName("仓库编号")]
        [Required(ErrorMessage = "请输入仓库编号")]
        public string StoreCode { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        [DisplayName("仓库名称")]
        [Required(ErrorMessage = "请输入仓库名称")]
        public string StoreName { get; set; }

        /// <summary>
        /// 仓库面积
        /// </summary>
        [DisplayName("仓库面积")]
        public decimal? Area { get; set; }

        /// <summary>
        /// 管理员姓名
        /// </summary>
        [DisplayName("管理员姓名")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Remark { get; set; }
    }
    public class BSStoreUpdate : BSStoreAdd
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [DisplayName("数据id")]
        [Required]
        public int Id { get; set; }
    }
}

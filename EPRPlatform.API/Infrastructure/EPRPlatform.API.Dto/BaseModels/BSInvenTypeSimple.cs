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
    /// 输出
    /// </summary>
    public class BSInvenTypeSimple
    {
        /// <summary>
        /// 数据id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 类别代码
        /// </summary>
        public string InvenTypeCode { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string InvenTypeName { get; set; }
        /// <summary>
        /// 有关联数据
        /// </summary>
        public bool IsHasInven { get; set; }
    }
}

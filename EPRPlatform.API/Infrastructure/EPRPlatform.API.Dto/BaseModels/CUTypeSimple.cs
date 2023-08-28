using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPRPlatform.API.Models.Base;
using StackExchange.Redis;

namespace EPRPlatform.API.Dto.BaseModels
{
    public class CUTypeSimple
    {
        /// <summary>
        /// 客户等级
        /// </summary>
        public List<CUGrade> Grade { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public List<CUState> State { get; set; }
        /// <summary>
        /// 信用
        /// </summary>
        public List<CUCredit> Credit { get; set; }
        /// <summary>
        /// 行业
        /// </summary>
        public List<CUTrade> Trade { get; set; }
        /// <summary>
        /// 员工
        /// </summary>
        public List<BSEmployee> Employees { get; set; }
    }
}

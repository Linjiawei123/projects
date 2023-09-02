using EPRPlatform.API.Models.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Dto.BaseModels
{
    public class BSStoreSimple : BSStore
    {
        /// <summary>
        /// 管理员姓名
        /// </summary>
        public string EmployeeName { get; set; }
    }
}

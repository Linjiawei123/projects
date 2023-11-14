using EPRPlatform.API.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Dto.BaseModels
{
    public class BSAccountSimple : BSAccount
    {
        /// <summary>
        /// 会计科目名称
        /// </summary>
        public string AccSubjectName { get; set; }
    }
}

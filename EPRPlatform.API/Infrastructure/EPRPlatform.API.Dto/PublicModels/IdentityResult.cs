using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Dto.PublicModels
{
    public class IdentityResult
    {
        /// <summary>
        /// 登录token
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 过期时长
        /// </summary>
        public int expires_in { get; set; }
        /// <summary>
        /// token类型
        /// </summary>
        public string token_type { get; set; }
        /// <summary>
        /// 刷新token
        /// </summary>
        public string refresh_token { get; set; }
        /// <summary>
        /// scope
        /// </summary>
        public string scope { get; set; }
    }
}

using EPRPlatform.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Dictionary.CustomModule
{
    public static class ModuleDictionaries
    {
        public static List<PublicModel<int, string>> Type => new()
        {
            new PublicModel<int, string>() { Key=1,Value="整数"},
            new PublicModel<int, string>() { Key=2,Value="带两位小数"},
            new PublicModel<int, string>() { Key=3,Value="字符串"},
            new PublicModel<int, string>() { Key=4,Value="手机号码"},
            new PublicModel<int, string>() { Key=5,Value="邮箱"},
            new PublicModel<int, string>() { Key=6,Value="文件"},
            new PublicModel<int, string>() { Key=7,Value="图片"},
        };
    }
}

using EPRPlatform.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Dictionary.Base
{
    public static class BaseDictionaries
    {
        public static List<PublicModel<int, string>> AccountType => new()
        {
            new PublicModel<int, string>() { Key=1,Value="现金"},
            new PublicModel<int, string>() { Key=2,Value="银行存款"}
        };
    }
}

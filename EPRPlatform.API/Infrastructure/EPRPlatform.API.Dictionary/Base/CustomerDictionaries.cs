using EPRPlatform.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Dictionary.Base
{
    public static class CustomerDictionaries
    {
        public static List<PublicModel<int, string>> Type => new()
        {
            new PublicModel<int, string>() { Key=1,Value="整数"},
        };
    }
}

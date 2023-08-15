using MicroService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Dto.OAModels.CustomModule
{
    public class CustomModuleDictionariesSimple
    {
        public List<PublicModel<int,string>> Type { get; set; }
    }
}

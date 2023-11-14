using EPRPlatform.API.Models.Highly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Interfaces.Highly
{
    public interface IGoodsRepository
    {
        Task<Goods> Get(Guid Id);
        Task<bool> Update(Goods goods, GoodsOrder goodsOrder);
    }
}

using EPRPlatform.API.Dto;
using EPRPlatform.API.Dto.PublicModels;
using EPRPlatform.API.Dto.RabbitModels;
using EPRPlatform.API.Extend;
using EPRPlatform.API.Interfaces;
using EPRPlatform.API.Interfaces.Highly;
using EPRPlatform.API.Models.Highly;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace ERPPlatform.API.Highly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        private readonly IErrorRepository _iError;
        private readonly IGoodsRepository _iGoods;
        private readonly IRedisInvoker _iRedis;
        private readonly IRabbitMQInvoker _iRabbitMQ;
        public GoodsController(IErrorRepository iError, IGoodsRepository iGoods, IRedisInvoker iRedis, IRabbitMQInvoker iRabbitMQ)
        {
            try
            {
                _iError = iError;
                _iGoods = iGoods;
                _iRedis = iRedis;
                _iRabbitMQ = iRabbitMQ;
            }
            catch (Exception ex)
            {
                _iError.AddErrorAsync(ex).Wait();
            }
        }
        [HttpGet("{Id}")]
        public async Task<OutPutModel<Goods>> GetAsync(Guid Id)
        {
            try
            {
                Goods data = null;
                data = await _iRedis.StringGetAsync<Goods>(Id.ToString());
                if (data == null)
                {
                    data = await _iGoods.Get(Id);
                    if (data != null)
                        await _iRedis.StringSetAsync<Goods>(Id.ToString(), data);
                }
                return OutPutMethod<Goods>.Success(data, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<Goods>.Failure();
            }
        }
        [HttpPost]
        public async Task<OutPutModel<object>> BuyAsync([FromBody] GoodsBuy input)
        {
            try
            {
                Goods data = null;
                data = await _iRedis.StringGetAsync<Goods>(input.GoodsId.ToString());
                if (data == null)
                    data = await _iGoods.Get(input.GoodsId);
                GoodsOrder order = new()
                {
                    Id = Guid.NewGuid(),
                    GoodsId = input.GoodsId,
                    Code = Guid.NewGuid().ToString(),
                    Number = input.Number,
                    Time = DateTime.Now,
                };
                data.Inventory = data.Inventory - order.Number;
                if(await _iGoods.Update(data, order))
                {
                    if (await _iRedis.KeyDeleteAsync(input.GoodsId.ToString()))
                    {
                        CacheMessage message = new()
                        {
                            CacheKey = input.GoodsId.ToString(),
                            Times = 1
                        };
                        string msg = JsonConvert.SerializeObject(message);
                        _iRabbitMQ.SendMsg("guest", "goods", msg);
                    }
                    return OutPutMethod<object>.Success();
                }
                return OutPutMethod<object>.Failure();
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
    }
    public class GoodsBuy
    {
        public Guid GoodsId { get; set; }
        public int Number { get; set; }
    }
}

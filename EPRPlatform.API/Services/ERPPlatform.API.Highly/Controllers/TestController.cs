using EPRPlatform.API.Extend;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPPlatform.API.Highly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMongoDBInvoker _invoker;
        public TestController(IMongoDBInvoker invoker)
        {
            _invoker = invoker;
        }
        [HttpGet]
        public async Task Test()
        {
            await _invoker.InsertOne<Test>(new Controllers.Test
            {
                Id = "2",
                Name = "测试"
            });
        }
    }
    class Test
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}

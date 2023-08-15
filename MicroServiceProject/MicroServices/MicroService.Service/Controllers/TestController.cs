using MicroService.Models;
using MicroService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroService.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : BaseController
    {
        private readonly IUserRepository _iUser;
        public TestController(IUserRepository iUser)
        {
            _iUser = iUser;
        }
        [HttpGet]
        public async Task<List<User>> Get()
        {
            return await _iUser.GetListAsync();
        }

        [HttpPost("test")]
        public async Task<XssTest> Port([FromForm] string str)
        {
            return new XssTest { str = str };
        }
        [HttpPost("body")]
        public async Task<string> Port([FromBody] XssTest input)
        {
            return input.str;
        }
        public class XssTest
        {
            public string str { get; set; }
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using EPRPlatform.API.Models;
using EPRPlatform.API.Method;
using EPRPlatform.API.Interfaces;
using EPRPlatform.API.Dto.OAModels;
using EPRPlatform.API.Dto.PublicModels;
using Microsoft.AspNetCore.Authorization;
using EPRPlatform.API.Extend;
using EPRPlatform.API.Dto;

namespace EPRPlatform.API.Host.Controllers
{
    /// <summary>
    /// 系统日志
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SystemLogController : BaseController
    {
        private readonly IErrorRepository _iError;
        private readonly ISystemLogRepository _iLog;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iError">错误</param>
        /// <param name="iLog">日志</param>
        public SystemLogController(IErrorRepository iError, ISystemLogRepository iLog)
        {
            try
            {
                _iError = iError;
                _iLog = iLog;
            }
            catch (Exception ex)
            {
                _iError.AddErrorAsync(ex).Wait();
            }
        }
        /// <summary>
        /// 登录日志分页
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="name">昵称</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        [HttpPost("LoginLog/{pageSize}/{pageIndex}")]
        [Permission("UserLoginLog_View")]
        public async Task<OutPutModel<PageModel<List<UserLoginLogSimple>>>> GetLoginLogPageAsync([FromForm] string account, [FromForm] string name, [FromForm] DateTime? beginTime, [FromForm] DateTime? endTime, short pageSize, int pageIndex)
        {
            try
            {
                PageModel<List<UserLoginLogSimple>> page = await _iLog.GetLoginLogPageAsync(account, name, beginTime, endTime, pageSize, pageIndex);
                return OutPutMethod<PageModel<List<UserLoginLogSimple>>>.Success(page, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<PageModel<List<UserLoginLogSimple>>>.Failure();
            }
        }
        /// <summary>
        /// 操作日志分页
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="name">昵称</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        [HttpPost("OperateLog/{pageSize}/{pageIndex}")]
        [Permission("OperateLog_View")]
        public async Task<OutPutModel<PageModel<List<OperateLogSimple>>>> GetOperateLogPageAsync([FromForm] string account, [FromForm] string name, [FromForm] DateTime? beginTime, [FromForm] DateTime? endTime, short pageSize, int pageIndex)
        {
            try
            {
                PageModel<List<OperateLogSimple>> page = await _iLog.GetOperateLogPageAsync(account, name, beginTime, endTime, pageSize, pageIndex);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<OperateLogSimple, OperateLogSimple>()
                    .ForMember(q => q.OperateName, p => p.MapFrom(w => PublicMethods.GetEnumDescription<OperateEnum>(w.Operate)));
                });
                IMapper mapper = config.CreateMapper();
                page.PageData = mapper.Map<List<OperateLogSimple>>(page.PageData);
                return OutPutMethod<PageModel<List<OperateLogSimple>>>.Success(page, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<PageModel<List<OperateLogSimple>>>.Failure();
            }
        }
    }
}

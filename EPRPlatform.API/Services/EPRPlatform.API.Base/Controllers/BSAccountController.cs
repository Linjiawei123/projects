using AutoMapper;
using EPRPlatform.API.Dictionary.Base;
using EPRPlatform.API.Dto;
using EPRPlatform.API.Dto.BaseModels;
using EPRPlatform.API.Dto.PublicModels;
using EPRPlatform.API.Extend;
using EPRPlatform.API.Interfaces;
using EPRPlatform.API.Method;
using EPRPlatform.API.Models;
using EPRPlatform.API.Models.Base;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace EPRPlatform.API.Base.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BSAccountController : ControllerBase
    {
        private readonly IErrorRepository _iError;
        private readonly IBSAccountRepository _iBSAccount;
        public BSAccountController(IErrorRepository iError, IBSAccountRepository iBSAccount)
        {
            try
            {
                _iError = iError;
                _iBSAccount = iBSAccount;
            }
            catch (Exception ex)
            {
                _iError.AddErrorAsync(ex).Wait();
            }
        }
        /// <summary>
        /// 获取分类
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetType")]
        public async Task<OutPutModel<List<PublicModel<int, string>>>> GetTypeAsync()
        {
            try
            {
                return OutPutMethod<List<PublicModel<int, string>>>.Success(BaseDictionaries.AccountType, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<List<PublicModel<int, string>>>.Failure();
            }
        }
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="AccountCode">存货编码</param>
        /// <param name="AccountName">存货名称</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        [HttpPost("{pageSize}/{pageIndex}")]
        public async Task<OutPutModel<PageModel<List<BSAccountSimple>>>> GetPageAsync([FromForm] string AccountCode, [FromForm] string AccountName, short pageSize, int pageIndex)
        {
            try
            {
                PageModel<List<BSAccount>> page = await _iBSAccount.GetPageAsync(AccountCode, AccountName, pageSize, pageIndex);
                PageModel<List<BSAccountSimple>> data = new()
                {
                    RecordCount = page.RecordCount,
                    PageCount = page.PageCount,
                    PageData = new()
                };
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<BSAccount, BSAccountSimple>()
                    .ForMember(p => p.AccSubjectName, q => q.MapFrom(w => PublicMethods.GetValueByKey(BaseDictionaries.AccountType, w.AccSubject) ?? ""));
                });
                IMapper mapper = config.CreateMapper();
                data.PageData = mapper.Map<List<BSAccountSimple>>(page.PageData);
                return OutPutMethod<PageModel<List<BSAccountSimple>>>.Success(data, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<PageModel<List<BSAccountSimple>>>.Failure();
            }
        }
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<OutPutModel<BSAccount>> GetAsync(long Id)
        {
            try
            {
                BSAccount data = await _iBSAccount.GetAsync(Id);
                if (data == null)
                    return OutPutMethod<BSAccount>.Failure("数据不存在", null, (int)HttpStatusCode.BadRequest);
                return OutPutMethod<BSAccount>.Success(data, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<BSAccount>.Failure();
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ModelFilter]
        public async Task<OutPutModel<object>> AddAsync([FromBody] BSAccountAdd input)
        {
            try
            {
                if (await _iBSAccount.AnyAsync(input.AccountCode))
                    return OutPutMethod<object>.Failure("编码重复，请重新设置", null, (int)HttpStatusCode.BadRequest);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<BSAccountAdd, BSAccount>();
                });
                IMapper mapper = config.CreateMapper();
                BSAccount obj = mapper.Map<BSAccount>(input);
                return await _iBSAccount.AddAsync(obj) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ModelFilter]
        public async Task<OutPutModel<object>> UpdateAsync([FromBody] BSAccountUpdate input)
        {
            try
            {
                BSAccount data = await _iBSAccount.GetAsync(input.Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("数据不存在", null, (int)HttpStatusCode.BadRequest);
                if (data.AccountCode != input.AccountCode && await _iBSAccount.AnyAsync(input.AccountCode))
                    return OutPutMethod<object>.Failure("编码重复，请重新设置", null, (int)HttpStatusCode.BadRequest);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<BSAccountUpdate, BSAccount>();
                });
                IMapper mapper = config.CreateMapper();
                BSAccount obj = mapper.Map<BSAccount>(input);
                return await _iBSAccount.UpdateAsync(obj) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<OutPutModel<object>> DeleteAsync(long Id)
        {
            try
            {
                BSAccount data = await _iBSAccount.GetAsync(Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("数据不存在", null, (int)HttpStatusCode.BadRequest);

                return await _iBSAccount.DeleteAsync(data) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
    }
}

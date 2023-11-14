using AutoMapper;
using EPRPlatform.API.Dto;
using EPRPlatform.API.Dto.BaseModels;
using EPRPlatform.API.Dto.PublicModels;
using EPRPlatform.API.Extend;
using EPRPlatform.API.Interfaces;
using EPRPlatform.API.Models;
using EPRPlatform.API.Models.Base;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace EPRPlatform.API.Base.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BSInvenController : ControllerBase
    {
        private readonly IErrorRepository _iError;
        private readonly IBSInvenRepository _iBSInven;
        private readonly IBSInvenTypeRepository _iBSInvenType;
        public BSInvenController(IErrorRepository iError, IBSInvenRepository iBSInven, IBSInvenTypeRepository iBSInvenType)
        {
            try
            {
                _iError = iError;
                _iBSInven = iBSInven;
                _iBSInvenType = iBSInvenType;
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
        public async Task<OutPutModel<List<BSInvenTypeSimple>>> GetTypeAsync()
        {
            try
            {
                List<BSInvenTypeSimple> list = await _iBSInvenType.GetListAsync();
                return OutPutMethod<List<BSInvenTypeSimple>>.Success(list,0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<List<BSInvenTypeSimple>>.Failure();
            }
        }
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="InvenCode">存货编码</param>
        /// <param name="InvenName">存货名称</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        [HttpPost("{pageSize}/{pageIndex}")]
        public async Task<OutPutModel<PageModel<List<BSInvenSimple>>>> GetPageAsync([FromForm] string InvenCode, [FromForm] string InvenName,short pageSize, int pageIndex)
        {
            try
            {
                PageModel<List<BSInvenSimple>> page = await _iBSInven.GetPageAsync(InvenCode, InvenName, pageSize, pageIndex);
                return OutPutMethod<PageModel<List<BSInvenSimple>>>.Success(page, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<PageModel<List<BSInvenSimple>>>.Failure();
            }
        }
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<OutPutModel<BSInvenSimple>> GetAsync(long Id)
        {
            try
            {
                BSInvenSimple data = await _iBSInven.GetAsync(Id);
                if (data == null)
                    return OutPutMethod<BSInvenSimple>.Failure("数据不存在", null, (int)HttpStatusCode.BadRequest);
                return OutPutMethod<BSInvenSimple>.Success(data, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<BSInvenSimple>.Failure();
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ModelFilter]
        public async Task<OutPutModel<object>> AddAsync([FromBody] BSInvenAdd input)
        {
            try
            {
                if (await _iBSInven.AnyAsync(input.InvenCode))
                    return OutPutMethod<object>.Failure("编码重复，请重新设置", null, (int)HttpStatusCode.BadRequest);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<BSInvenAdd, BSInven>();
                });
                IMapper mapper = config.CreateMapper();
                BSInven obj = mapper.Map<BSInven>(input);
                return await _iBSInven.AddAsync(obj) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
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
        public async Task<OutPutModel<object>> UpdateAsync([FromBody] BSInvenUpdate input)
        {
            try
            {
                BSInvenSimple data = await _iBSInven.GetAsync(input.Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("数据不存在", null, (int)HttpStatusCode.BadRequest);
                if (data.InvenCode != input.InvenCode && await _iBSInven.AnyAsync(input.InvenCode))
                    return OutPutMethod<object>.Failure("编码重复，请重新设置", null, (int)HttpStatusCode.BadRequest);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<BSInvenUpdate, BSInven>();
                });
                IMapper mapper = config.CreateMapper();
                BSInven obj = mapper.Map<BSInven>(input);
                return await _iBSInven.UpdateAsync(obj) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
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
                BSInvenSimple data = await _iBSInven.GetAsync(Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("数据不存在", null, (int)HttpStatusCode.BadRequest);

                return await _iBSInven.DeleteAsync(data) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
            }
            catch(Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
    }
}

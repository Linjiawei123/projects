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
    public class BSStoreController : ControllerBase
    {
        private readonly IErrorRepository _iError;
        private readonly IBSStoreRepository _iBSStore;
        public BSStoreController(IErrorRepository iError, IBSStoreRepository iBSStore)
        {
            try
            {
                _iError = iError;
                _iBSStore = iBSStore;
            }
            catch (Exception ex)
            {
                _iError.AddErrorAsync(ex).Wait();
            }
        }
        /// <summary>
        /// 获取职工列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("Employee")]
        public async Task<OutPutModel<List<BSEmployee>>> GetEmployeeAsync()
        {
            try
            {
                List<BSEmployee> data = await _iBSStore.GetEmployeesAsync();
                return OutPutMethod<List<BSEmployee>>.Success(data, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<List<BSEmployee>>.Failure();
            }
        }
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="StoreCode">仓库编码</param>
        /// <param name="StoreName">仓库名称</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        [HttpPost("{pageSize}/{pageIndex}")]
        public async Task<OutPutModel<PageModel<List<BSStoreSimple>>>> GetPageAsync([FromForm] string StoreCode, [FromForm] string StoreName, short pageSize, int pageIndex)
        {
            try
            {
                PageModel<List<BSStoreSimple>> page = await _iBSStore.GetPageAsync(StoreCode, StoreName, pageSize, pageIndex);
                return OutPutMethod<PageModel<List<BSStoreSimple>>>.Success(page, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<PageModel<List<BSStoreSimple>>>.Failure();
            }
        }
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<OutPutModel<BSStoreSimple>> GetAsync(long Id)
        {
            try
            {
                BSStoreSimple data = await _iBSStore.GetAsync(Id);
                if (data == null)
                    return OutPutMethod<BSStoreSimple>.Failure("数据不存在", null, (int)HttpStatusCode.BadRequest);
                return OutPutMethod<BSStoreSimple>.Success(data, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<BSStoreSimple>.Failure();
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ModelFilter]
        public async Task<OutPutModel<object>> AddAsync([FromBody] BSStoreAdd input)
        {
            try
            {
                if (await _iBSStore.AnyAsync(input.StoreCode))
                    return OutPutMethod<object>.Failure("编码重复，请重新设置", null, (int)HttpStatusCode.BadRequest);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<BSStoreAdd, BSStore>();
                });
                IMapper mapper = config.CreateMapper();
                BSStore obj = mapper.Map<BSStore>(input);
                return await _iBSStore.AddAsync(obj) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
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
        public async Task<OutPutModel<object>> UpdateAsync([FromBody] BSStoreUpdate input)
        {
            try
            {
                BSStoreSimple data = await _iBSStore.GetAsync(input.Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("数据不存在", null, (int)HttpStatusCode.BadRequest);
                if (data.StoreCode != input.StoreCode && await _iBSStore.AnyAsync(input.StoreCode))
                    return OutPutMethod<object>.Failure("编码重复，请重新设置", null, (int)HttpStatusCode.BadRequest);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<BSStoreUpdate, BSStore>();
                });
                IMapper mapper = config.CreateMapper();
                BSStore obj = mapper.Map<BSStore>(input);
                return await _iBSStore.UpdateAsync(obj) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
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
                BSStoreSimple data = await _iBSStore.GetAsync(Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("数据不存在", null, (int)HttpStatusCode.BadRequest);

                return await _iBSStore.DeleteAsync(data) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
    }
}

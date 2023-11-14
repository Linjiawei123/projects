using AutoMapper;
using EPRPlatform.API.Dto;
using EPRPlatform.API.Dto.BaseModels;
using EPRPlatform.API.Dto.PublicModels;
using EPRPlatform.API.Extend;
using EPRPlatform.API.Interfaces;
using EPRPlatform.API.Models;
using EPRPlatform.API.Models.Base;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EPRPlatform.API.Base.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BSSupplierController : ControllerBase
    {
        private readonly IErrorRepository _iError;
        private readonly IBSSupplierRepository _iBSSupplier;
        public BSSupplierController(IErrorRepository iError, IBSSupplierRepository iBSSupplier)
        {
            try
            {
                _iError = iError;
                _iBSSupplier = iBSSupplier;
            }
            catch (Exception ex)
            {
                _iError.AddErrorAsync(ex).Wait();
            }
        }
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="SupplierCode">供应商编号</param>
        /// <param name="SupplierName">供应商名称</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        [HttpPost("{pageSize}/{pageIndex}")]
        public async Task<OutPutModel<PageModel<List<BSSupplier>>>> GetPageAsync([FromForm] string SupplierCode, [FromForm] string SupplierName, short pageSize, int pageIndex)
        {
            try
            {
                PageModel<List<BSSupplier>> page = await _iBSSupplier.GetPageAsync(SupplierCode, SupplierName, pageSize, pageIndex);
                return OutPutMethod<PageModel<List<BSSupplier>>>.Success(page, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<PageModel<List<BSSupplier>>>.Failure();
            }
        }
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<OutPutModel<BSSupplier>> GetAsync(long Id)
        {
            try
            {
                BSSupplier data = await _iBSSupplier.GetAsync(Id);
                if (data == null)
                    return OutPutMethod<BSSupplier>.Failure("数据不存在", null, (int)HttpStatusCode.BadRequest);
                return OutPutMethod<BSSupplier>.Success(data, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<BSSupplier>.Failure();
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ModelFilter]
        public async Task<OutPutModel<object>> AddAsync([FromBody] BSSupplierAdd input)
        {
            try
            {
                if (await _iBSSupplier.AnyAsync(input.SupplierCode))
                    return OutPutMethod<object>.Failure("编码重复，请重新设置", null, (int)HttpStatusCode.BadRequest);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<BSSupplierAdd, BSSupplier>();
                });
                IMapper mapper = config.CreateMapper();
                BSSupplier obj = mapper.Map<BSSupplier>(input);
                return await _iBSSupplier.AddAsync(obj) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
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
        public async Task<OutPutModel<object>> UpdateAsync([FromBody] BSSupplierUpdate input)
        {
            try
            {
                BSSupplier data = await _iBSSupplier.GetAsync(input.Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("数据不存在", null, (int)HttpStatusCode.BadRequest);
                if (data.SupplierCode != input.SupplierCode && await _iBSSupplier.AnyAsync(input.SupplierCode))
                    return OutPutMethod<object>.Failure("编码重复，请重新设置", null, (int)HttpStatusCode.BadRequest);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<BSSupplierUpdate, BSSupplier>();
                });
                IMapper mapper = config.CreateMapper();
                BSSupplier obj = mapper.Map<BSSupplier>(input);
                return await _iBSSupplier.UpdateAsync(obj) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
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
                BSSupplier data = await _iBSSupplier.GetAsync(Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("数据不存在", null, (int)HttpStatusCode.BadRequest);

                return await _iBSSupplier.DeleteAsync(data) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
    }
}

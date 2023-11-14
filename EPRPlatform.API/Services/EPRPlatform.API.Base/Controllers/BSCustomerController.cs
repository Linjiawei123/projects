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
    public class BSCustomerController : ControllerBase
    {
        private readonly IErrorRepository _iError;
        private readonly IBSCustomerRepository _iBSCustomer;
        public BSCustomerController(IErrorRepository iError, IBSCustomerRepository iBSCustomer)
        {
            try
            {
                _iError = iError;
                _iBSCustomer = iBSCustomer;
            }
            catch (Exception ex)
            {
                _iError.AddErrorAsync(ex).Wait();
            }
        }
        /// <summary>
        /// 获取类型
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetType")]
        public async Task<OutPutModel<CUTypeSimple>> GetTypesAsync()
        {
            try
            {
                CUTypeSimple data = await _iBSCustomer.GetOtherAsync();
                return OutPutMethod<CUTypeSimple>.Success(data, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<CUTypeSimple>.Failure();
            }
        }
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="CustomerCode">客户编号</param>
        /// <param name="CustomerName">客户名称</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        [HttpPost("{pageSize}/{pageIndex}")]
        public async Task<OutPutModel<PageModel<List<BSCustomer>>>> GetPageAsync([FromForm] string CustomerCode, [FromForm] string CustomerName, short pageSize, int pageIndex)
        {
            try
            {
                PageModel<List<BSCustomer>> page = await _iBSCustomer.GetPageAsync(CustomerCode, CustomerName, pageSize, pageIndex);
                return OutPutMethod<PageModel<List<BSCustomer>>>.Success(page, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<PageModel<List<BSCustomer>>>.Failure();
            }
        }
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<OutPutModel<BSCustomer>> GetAsync(long Id)
        {
            try
            {
                BSCustomer data = await _iBSCustomer.GetAsync(Id);
                if (data == null)
                    return OutPutMethod<BSCustomer>.Failure("数据不存在", null, (int)HttpStatusCode.BadRequest);
                return OutPutMethod<BSCustomer>.Success(data, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<BSCustomer>.Failure();
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ModelFilter]
        public async Task<OutPutModel<object>> AddAsync([FromBody] BSCustomerAdd input)
        {
            try
            {
                if (await _iBSCustomer.AnyAsync(input.CustomerCode))
                    return OutPutMethod<object>.Failure("编码重复，请重新设置", null, (int)HttpStatusCode.BadRequest);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<BSCustomerAdd, BSCustomer>();
                });
                IMapper mapper = config.CreateMapper();
                BSCustomer obj = mapper.Map<BSCustomer>(input);
                return await _iBSCustomer.AddAsync(obj) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
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
        public async Task<OutPutModel<object>> UpdateAsync([FromBody] BSCustomerUpdate input)
        {
            try
            {
                BSCustomer data = await _iBSCustomer.GetAsync(input.Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("数据不存在", null, (int)HttpStatusCode.BadRequest);
                if (data.CustomerCode != input.CustomerCode && await _iBSCustomer.AnyAsync(input.CustomerCode))
                    return OutPutMethod<object>.Failure("编码重复，请重新设置", null, (int)HttpStatusCode.BadRequest);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<BSCustomerUpdate, BSCustomer>();
                });
                IMapper mapper = config.CreateMapper();
                BSCustomer obj = mapper.Map<BSCustomer>(input);
                return await _iBSCustomer.UpdateAsync(obj) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
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
                BSCustomer data = await _iBSCustomer.GetAsync(Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("数据不存在", null, (int)HttpStatusCode.BadRequest);

                return await _iBSCustomer.DeleteAsync(data) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
    }
}

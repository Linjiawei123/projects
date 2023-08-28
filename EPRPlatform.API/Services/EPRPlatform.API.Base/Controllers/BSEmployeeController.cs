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
    public class BSEmployeeController : ControllerBase
    {
        private readonly IErrorRepository _iError;
        private readonly IBSEmployeeRepository _iBSEmployee;
        public BSEmployeeController(IErrorRepository iError, IBSEmployeeRepository iBSEmployee)
        {
            try
            {
                _iError = iError;
                _iBSEmployee = iBSEmployee;
            }
            catch (Exception ex)
            {
                _iError.AddErrorAsync(ex).Wait();
            }
        }
        [HttpGet("dic")]
        public async Task<OutPutModel<INSimple>> GetAsync()
        {
            try
            {
                INSimple data = await _iBSEmployee.GetOtherAsync();
                return OutPutMethod<INSimple>.Success(data,0);
            }catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<INSimple>.Failure();
            }
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="EmployeeCode">供应商编号</param>
        /// <param name="EmployeeName">供应商名称</param>
        /// <param name="TelephoneCode">联系电话</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        [HttpPost("{pageSize}/{pageIndex}")]
        public async Task<OutPutModel<PageModel<List<BSEmployeeSimple>>>> GetPageAsync([FromForm] string EmployeeCode, [FromForm] string EmployeeName, [FromForm] string TelephoneCode, short pageSize, int pageIndex)
        {
            try
            {
                PageModel<List<BSEmployeeSimple>> page = await _iBSEmployee.GetPageAsync(EmployeeCode, EmployeeName, TelephoneCode, pageSize, pageIndex);
                return OutPutMethod<PageModel<List<BSEmployeeSimple>>>.Success(page, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<PageModel<List<BSEmployeeSimple>>>.Failure();
            }
        }
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<OutPutModel<BSEmployeeSimple>> GetAsync(long Id)
        {
            try
            {
                BSEmployeeSimple data = await _iBSEmployee.GetAsync(Id);
                if (data == null)
                    return OutPutMethod<BSEmployeeSimple>.Failure("数据不存在", null, (int)HttpStatusCode.BadRequest);
                return OutPutMethod<BSEmployeeSimple>.Success(data, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<BSEmployeeSimple>.Failure();
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ModelFilter]
        public async Task<OutPutModel<object>> AddAsync([FromBody] BSEmployeeAdd input)
        {
            try
            {
                if (await _iBSEmployee.AnyAsync(input.EmployeeCode))
                    return OutPutMethod<object>.Failure("编码重复，请重新设置", null, (int)HttpStatusCode.BadRequest);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<BSEmployeeAdd, BSEmployee>();
                });
                IMapper mapper = config.CreateMapper();
                BSEmployee obj = mapper.Map<BSEmployee>(input);
                return await _iBSEmployee.AddAsync(obj) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
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
        public async Task<OutPutModel<object>> UpdateAsync([FromBody] BSEmployeeUpdate input)
        {
            try
            {
                BSEmployee data = await _iBSEmployee.GetAsync(input.Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("数据不存在", null, (int)HttpStatusCode.BadRequest);
                if (data.EmployeeCode != input.EmployeeCode && await _iBSEmployee.AnyAsync(input.EmployeeCode))
                    return OutPutMethod<object>.Failure("编码重复，请重新设置", null, (int)HttpStatusCode.BadRequest);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<BSEmployeeUpdate, BSEmployee>();
                });
                IMapper mapper = config.CreateMapper();
                BSEmployee obj = mapper.Map<BSEmployee>(input);
                return await _iBSEmployee.UpdateAsync(obj) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
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
                BSEmployee data = await _iBSEmployee.GetAsync(Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("数据不存在", null, (int)HttpStatusCode.BadRequest);

                return await _iBSEmployee.DeleteAsync(data) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
    }
}

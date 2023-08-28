using EPRPlatform.API.Dto.BaseModels;
using EPRPlatform.API.Dto.PublicModels;
using EPRPlatform.API.Dto;
using EPRPlatform.API.Interfaces;
using EPRPlatform.API.Models.Base;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using EPRPlatform.API.Extend;

namespace EPRPlatform.API.Base.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BSDepartmentController : ControllerBase
    {
        private readonly IErrorRepository _iError;
        private readonly IBSDepartmentRepository _iBSDepartment;
        /// <summary>
        /// 
        /// </summary>
        public BSDepartmentController(IErrorRepository iError, IBSDepartmentRepository iBSDepartment)
        {
            try
            {
                _iError = iError;
                _iBSDepartment = iBSDepartment;
            }
            catch (Exception ex)
            {
                _iError.AddErrorAsync(ex).Wait();
            }
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<OutPutModel<List<BSDepartmentSimple>>> GetListAsync()
        {
            try
            {
                List<BSDepartmentSimple> list = await _iBSDepartment.GetListAsync();
                return OutPutMethod<List<BSDepartmentSimple>>.Success(list, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<List<BSDepartmentSimple>>.Failure();
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input">输入实体</param>
        /// <returns></returns>
        [HttpPost]
        [ModelFilter]
        public async Task<OutPutModel<object>> AddAsync([FromBody] BSDepartment input)
        {
            try
            {
                if (await _iBSDepartment.AnyAsync(input.DepartmentCode))
                    return OutPutMethod<object>.Failure("编码重复，请重新设置", null, (int)HttpStatusCode.BadRequest);

                return await _iBSDepartment.AddAsync(input) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
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
        /// <param name="input">输入实体</param>
        /// <returns></returns>
        [HttpPut]
        [ModelFilter]
        public async Task<OutPutModel<object>> UpdateAsync([FromBody] BSDepartment input)
        {
            try
            {
                BSDepartment data = await _iBSDepartment.GetAsync(input.Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("类别不存在", null, (int)HttpStatusCode.BadRequest);
                if (data.BSEmployees != null && data.BSEmployees.Count > 0)
                    return OutPutMethod<object>.Failure("存在关联数据，不可修改", null, (int)HttpStatusCode.BadRequest);
                if (data.DepartmentCode != input.DepartmentCode && await _iBSDepartment.AnyAsync(input.DepartmentCode))
                    return OutPutMethod<object>.Failure("编码重复，请重新设置", null, (int)HttpStatusCode.BadRequest);

                return await _iBSDepartment.UpdateAsync(input) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
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
        [HttpDelete]
        public async Task<OutPutModel<object>> DeleteAsync(int Id)
        {
            try
            {
                BSDepartment data = await _iBSDepartment.GetAsync(Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("类别不存在", null, (int)HttpStatusCode.BadRequest);
                if (data.BSEmployees != null && data.BSEmployees.Count > 0)
                    return OutPutMethod<object>.Failure("存在关联数据，不可删除", null, (int)HttpStatusCode.BadRequest);
                return await _iBSDepartment.DeleteAsync(data) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
    }
}

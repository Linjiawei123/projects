using EPRPlatform.API.Dto;
using EPRPlatform.API.Dto.BaseModels;
using EPRPlatform.API.Dto.PublicModels;
using EPRPlatform.API.Extend;
using EPRPlatform.API.Interfaces;
using EPRPlatform.API.Models.Base;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EPRPlatform.API.Base.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BSCostTypeController : ControllerBase
    {
        private readonly IErrorRepository _iError;
        private readonly IBSCostTypeRepository _iBSCostType;
        /// <summary>
        /// 
        /// </summary>
        public BSCostTypeController(IErrorRepository iError, IBSCostTypeRepository iBSCostType)
        {
            try
            {
                _iError = iError;
                _iBSCostType = iBSCostType;
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
        public async Task<OutPutModel<List<BSCostTypeSimple>>> GetListAsync()
        {
            try
            {
                List<BSCostTypeSimple> list = await _iBSCostType.GetListAsync();
                return OutPutMethod<List<BSCostTypeSimple>>.Success(list, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<List<BSCostTypeSimple>>.Failure();
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input">输入实体</param>
        /// <returns></returns>
        [HttpPost]
        [ModelFilter]
        public async Task<OutPutModel<object>> AddAsync([FromBody] BSCostType input)
        {
            try
            {
                if (await _iBSCostType.AnyAsync(input.CostTypeCode))
                    return OutPutMethod<object>.Failure("编码重复，请重新设置", null, (int)HttpStatusCode.BadRequest);

                return await _iBSCostType.AddAsync(input) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
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
        public async Task<OutPutModel<object>> UpdateAsync([FromBody] BSCostType input)
        {
            try
            {
                BSCostType data = await _iBSCostType.GetAsync(input.Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("类别不存在", null, (int)HttpStatusCode.BadRequest);
                if (data.BSCosts != null && data.BSCosts.Count > 0)
                    return OutPutMethod<object>.Failure("存在关联数据，不可修改", null, (int)HttpStatusCode.BadRequest);
                if (data.CostTypeCode != input.CostTypeCode && await _iBSCostType.AnyAsync(input.CostTypeCode))
                    return OutPutMethod<object>.Failure("编码重复，请重新设置", null, (int)HttpStatusCode.BadRequest);

                return await _iBSCostType.UpdateAsync(input) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
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
                BSCostType data = await _iBSCostType.GetAsync(Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("类别不存在", null, (int)HttpStatusCode.BadRequest);
                if (data.BSCosts != null && data.BSCosts.Count > 0)
                    return OutPutMethod<object>.Failure("存在关联数据，不可删除", null, (int)HttpStatusCode.BadRequest);
                return await _iBSCostType.DeleteAsync(data) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
    }
}

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
    public class BSInvenTypeController : ControllerBase
    {
        private readonly IErrorRepository _iError;
        private readonly IBSInvenTypeRepository _iBSInvenType;
        /// <summary>
        /// 
        /// </summary>
        public BSInvenTypeController(IErrorRepository iError, IBSInvenTypeRepository iBSInvenType)
        {
            try
            {
                _iError = iError;
                _iBSInvenType = iBSInvenType;
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
        public async Task<OutPutModel<List<BSInvenTypeSimple>>> GetListAsync()
        {
            try
            {
                List<BSInvenTypeSimple> list = await _iBSInvenType.GetListAsync();
                return OutPutMethod<List<BSInvenTypeSimple>>.Success(list, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<List<BSInvenTypeSimple>>.Failure();
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input">输入实体</param>
        /// <returns></returns>
        [HttpPost]
        [ModelFilter]
        public async Task<OutPutModel<object>> AddAsync([FromBody] BSInvenType input)
        {
            try
            {
                if (await _iBSInvenType.AnyAsync(input.InvenTypeCode))
                    return OutPutMethod<object>.Failure("编码重复，请重新设置", null, (int)HttpStatusCode.BadRequest);

                return await _iBSInvenType.AddAsync(input) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
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
        public async Task<OutPutModel<object>> UpdateAsync([FromBody] BSInvenType input)
        {
            try
            {
                BSInvenType data = await _iBSInvenType.GetAsync(input.Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("类别不存在", null, (int)HttpStatusCode.BadRequest);
                if (data.BSInvens != null && data.BSInvens.Count > 0)
                    return OutPutMethod<object>.Failure("存在关联数据，不可修改", null, (int)HttpStatusCode.BadRequest);
                if (data.InvenTypeCode != input.InvenTypeCode && await _iBSInvenType.AnyAsync(input.InvenTypeCode))
                    return OutPutMethod<object>.Failure("编码重复，请重新设置", null, (int)HttpStatusCode.BadRequest);

                return await _iBSInvenType.UpdateAsync(input) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
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
                BSInvenType data = await _iBSInvenType.GetAsync(Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("类别不存在", null, (int)HttpStatusCode.BadRequest);
                if (data.BSInvens != null && data.BSInvens.Count > 0)
                    return OutPutMethod<object>.Failure("存在关联数据，不可删除", null, (int)HttpStatusCode.BadRequest);
                return await _iBSInvenType.DeleteAsync(data) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
    }
}

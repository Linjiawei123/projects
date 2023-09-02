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
    public class BSCostController : ControllerBase
    {
        private readonly IErrorRepository _iError;
        private readonly IBSCostRepository _iBSCost;
        private readonly IBSCostTypeRepository _iBSCostType;
        public BSCostController(IErrorRepository iError, IBSCostRepository iBSCost, IBSCostTypeRepository iBSCostType)
        {
            try
            {
                _iError = iError;
                _iBSCost = iBSCost;
                _iBSCostType = iBSCostType;
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
        public async Task<OutPutModel<List<BSCostTypeSimple>>> GetTypeAsync()
        {
            try
            {
                List<BSCostTypeSimple> list = await _iBSCostType.GetListAsync();
                return OutPutMethod<List<BSCostTypeSimple>>.Success(list,0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<List<BSCostTypeSimple>>.Failure();
            }
        }
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="CostCode">存货编码</param>
        /// <param name="CostName">存货名称</param>
        /// <param name="CostTypeCode">存货类别</param>
        /// <param name="SpecsModel">规格型号</param>
        /// <param name="MeaUnit">计量单位</param>
        /// <param name="SelPrice">参考售价</param>
        /// <param name="PurPrice">参考进价</param>
        /// <param name="SmallStockNum">最低库存</param>
        /// <param name="BigStockNum">最高库存</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        [HttpPost("{pageSize}/{pageIndex}")]
        public async Task<OutPutModel<PageModel<List<BSCostSimple>>>> GetPageAsync([FromForm] string CostCode, [FromForm] string CostName, [FromForm] string CostTypeCode, short pageSize, int pageIndex)
        {
            try
            {
                PageModel<List<BSCostSimple>> page = await _iBSCost.GetPageAsync(CostCode, CostName, CostTypeCode, pageSize, pageIndex);
                return OutPutMethod<PageModel<List<BSCostSimple>>>.Success(page, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<PageModel<List<BSCostSimple>>>.Failure();
            }
        }
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<OutPutModel<BSCostSimple>> GetAsync(long Id)
        {
            try
            {
                BSCostSimple data = await _iBSCost.GetAsync(Id);
                if (data == null)
                    return OutPutMethod<BSCostSimple>.Failure("数据不存在", null, (int)HttpStatusCode.BadRequest);
                return OutPutMethod<BSCostSimple>.Success(data, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<BSCostSimple>.Failure();
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ModelFilter]
        public async Task<OutPutModel<object>> AddAsync([FromBody] BSCostAdd input)
        {
            try
            {
                if (await _iBSCost.AnyAsync(input.CostCode))
                    return OutPutMethod<object>.Failure("编码重复，请重新设置", null, (int)HttpStatusCode.BadRequest);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<BSCostAdd, BSCost>();
                });
                IMapper mapper = config.CreateMapper();
                BSCost obj = mapper.Map<BSCost>(input);
                return await _iBSCost.AddAsync(obj) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
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
        public async Task<OutPutModel<object>> UpdateAsync([FromBody] BSCostUpdate input)
        {
            try
            {
                BSCostSimple data = await _iBSCost.GetAsync(input.Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("数据不存在", null, (int)HttpStatusCode.BadRequest);
                if (data.CostCode != input.CostCode && await _iBSCost.AnyAsync(input.CostCode))
                    return OutPutMethod<object>.Failure("编码重复，请重新设置", null, (int)HttpStatusCode.BadRequest);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<BSCostUpdate, BSCost>();
                });
                IMapper mapper = config.CreateMapper();
                BSCost obj = mapper.Map<BSCost>(input);
                return await _iBSCost.UpdateAsync(obj) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
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
                BSCostSimple data = await _iBSCost.GetAsync(Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("数据不存在", null, (int)HttpStatusCode.BadRequest);

                return await _iBSCost.DeleteAsync(data) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
            }
            catch(Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
    }
}

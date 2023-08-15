using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicroService.Models;
using  MicroService.Dto.OAModels;
using MicroService.Repository;
using MicroService.Interfaces;
using System.Net;
using MicroService.Dto.PublicModels;
using MicroService.Extend;

namespace MicroService.OA.Controllers
{
    /// <summary>
    /// 职工合同
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [FieldFilter]
    [Authorize]
    public class EmployeeContractController : BaseController
    {
        private const int _module = 10007;
        private readonly IErrorRepository _iError;
        private readonly IEmployeeContractRepository _iEmployeeContract;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iError"></param>
        /// <param name="iEmployeeContract"></param>
        public EmployeeContractController(IErrorRepository iError, IEmployeeContractRepository iEmployeeContract)
        {
            try
            {
                _iError = iError;
                _iEmployeeContract = iEmployeeContract;
            }
            catch (Exception ex)
            {
                _iError.AddErrorAsync(ex).Wait();
            }
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="code">合同编号</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        [HttpPost("{pageSize}/{pageIndex}")]
        [Permission("EmployeeContract_View")]
        public async Task<OutPutModel<PageModel<List<EmployeeContractOutData>>>> GetPageAsync([FromForm] string name, [FromForm] string code, short pageSize, int pageIndex)
        {
            try
            {
                PageModel<List<EmployeeContractSimple>> page = await _iEmployeeContract.GetPageAsync(name, code, pageSize, pageIndex);
                PageModel<List<EmployeeContractOutData>> data = new()
                {
                    RecordCount = page.RecordCount,
                    PageCount = page.PageCount,
                    PageData = new List<EmployeeContractOutData>()
                };
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<EmployeeContractSimple, EmployeeContractOutData>()
                    .ForMember(q => q.StartDate, p => p.MapFrom(w => w.StartDate.ToString("yyyy-MM-dd")))
                    .ForMember(q => q.EndDate, p => p.MapFrom(w => w.EndDate.HasValue ? w.EndDate.Value.ToString("yyyy-MM-dd") : ""))
                    .ForMember(q => q.SigningDate, p => p.MapFrom(w => w.SigningDate.ToString("yyyy-MM-dd")));
                });
                IMapper mapper = config.CreateMapper();
                data.PageData = mapper.Map<List<EmployeeContractOutData>>(page.PageData);
                return OutPutMethod<PageModel<List<EmployeeContractOutData>>>.Success(data, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<PageModel<List<EmployeeContractOutData>>>.Failure();
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input">输入实体</param>
        /// <returns></returns>
        [HttpPost]
        [ModelFilter]
        [Permission("EmployeeContract_Add")]
        [TypeFilter(typeof(OperateLogAttribute), Arguments = new object[] { _module, OperateEnum.Add })]
        public async Task<OutPutModel<object>> AddAsync([FromBody] EmployeeContractAdd input)
        {
            try
            {
                
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<EmployeeContractAdd, EmployeeContract>();
                });
                IMapper mapper = config.CreateMapper();
                EmployeeContract obj = mapper.Map<EmployeeContract>(input);
                obj.Id = Guid.NewGuid();
                obj.OperaterId = UserInformation.Id;
                obj.OperateTime = DateTime.Now;
                return await _iEmployeeContract.AddAsync(obj) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
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
        [Permission("EmployeeContract_Update")]
        [TypeFilter(typeof(OperateLogAttribute), Arguments = new object[] { _module, OperateEnum.Update })]
        public async Task<OutPutModel<object>> UpdateAsync([FromBody] EmployeeContractUpdate input)
        {
            try
            {
                EmployeeContractSimple data = await _iEmployeeContract.GetAsync(input.Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("职工合同信息不存在", null, (int)HttpStatusCode.NotFound);
              
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<EmployeeContractUpdate, EmployeeContract>();
                });
                IMapper mapper = config.CreateMapper();
                EmployeeContract obj = mapper.Map<EmployeeContract>(input);
                obj.OperaterId = UserInformation.Id;
                obj.OperateTime = DateTime.Now;
                return await _iEmployeeContract.UpdateAsync(obj) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
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
        [Permission("EmployeeContract_Delete")]
        [TypeFilter(typeof(OperateLogAttribute), Arguments = new object[] { _module, OperateEnum.Delete })]
        public async Task<OutPutModel<object>> DeleteAsync(Guid Id)
        {
            try
            {
                EmployeeContractSimple data = await _iEmployeeContract.GetAsync(Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("职工合同信息不存在", null, (int)HttpStatusCode.NotFound);
                return await _iEmployeeContract.DeleteAsync(data) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
    }
}

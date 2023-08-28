using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EPRPlatform.API.Models;
using EPRPlatform.API.Dto.OAModels;
using EPRPlatform.API.Interfaces;
using System.Net;
using EPRPlatform.API.Dto.PublicModels;
using EPRPlatform.API.Extend;
using EPRPlatform.API.Dto;

namespace EPRPlatform.API.Host.Controllers
{
    /// <summary>
    /// 部门管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [FieldFilter]
    [Authorize]
    public class DepartmentController : BaseController
    {
        private const int _module = 10005;
        private readonly IErrorRepository _iError;
        private readonly IDepartmentRepository _iDepartment;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iError">错误</param>
        /// <param name="iDepartment">部门</param>
        public DepartmentController(IErrorRepository iError, IDepartmentRepository iDepartment)
        {
            try
            {
                _iError = iError;
                _iDepartment = iDepartment;
            }
            catch (Exception ex)
            {
                _iError.AddErrorAsync(ex).Wait();
            }
        }
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="Name">名称</param>
        /// <returns></returns>
        [HttpGet]
        [Permission("Department_View")]
        public async Task<OutPutModel<List<DepartmentSimple>>> GetListAsync([FromQuery] string Name)
        {
            try
            {
                List<Department> list = await _iDepartment.GetListAsync();
                List<DepartmentSimple> mlist = new();
                foreach (var item in list)
                {
                    mlist.Add(new DepartmentSimple()
                    {
                        Id = item.Id,
                        ParentId = item.ParentId,
                        Name = item.Name,
                        Code = item.Code,
                        Child = new List<DepartmentSimple>()
                    });
                }
                List<DepartmentSimple> topList = new();
                List<DepartmentSimple> childList = new();
                if (string.IsNullOrEmpty(Name))
                {
                    topList = mlist.FindAll(w => w.ParentId == Guid.Empty);//最高级菜单
                    childList = mlist.FindAll(w => w.ParentId != Guid.Empty);//所有子菜单
                }
                else
                {
                    topList = mlist.FindAll(w => w.Name.Contains(Name));//最高级菜单
                }
                foreach (var item in topList)
                {
                    AddChildMenu(item, childList);
                }
                return OutPutMethod<List<DepartmentSimple>>.Success(topList, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<List<DepartmentSimple>>.Failure();
            }
        }

        private void AddChildMenu(DepartmentSimple parent, List<DepartmentSimple> childList)
        {
            var childs = childList.FindAll(w => w.ParentId == parent.Id);
            if (childs != null && childs.Count > 0)
            {
                foreach (var item in childs)
                {
                    parent.Child.Add(item);
                    AddChildMenu(item, childList);
                }
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input">输入实体</param>
        /// <returns></returns>
        [HttpPost]
        [ModelFilter]
        [Permission("Department_Add")]
        [TypeFilter(typeof(OperateLogAttribute), Arguments = new object[] { _module, OperateEnum.Add })]
        public async Task<OutPutModel<object>> AddAsync([FromBody] DepartmentAdd input)
        {
            try
            {
                if (await _iDepartment.ExistsByCode(input.Code, null))
                    return OutPutMethod<object>.Failure("该部门编号已被使用", null, (int)HttpStatusCode.BadRequest);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<DepartmentAdd, Department>();
                });
                IMapper mapper = config.CreateMapper();
                Department data = mapper.Map<Department>(input);
                data.Id = Guid.NewGuid();
                data.OperaterId = UserInformation.Id;
                data.OperateTime = DateTime.Now;
                if (input.ParentId == null)
                    data.ParentId = Guid.Empty;
                else
                {
                    var parent = await _iDepartment.GetByIdAsync(data.ParentId);
                    if(parent == null)
                        data.ParentId = Guid.Empty;
                }
                return await _iDepartment.AddAsync(data) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
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
        [Permission("Department_Update")]
        [TypeFilter(typeof(OperateLogAttribute), Arguments = new object[] { _module, OperateEnum.Update })]
        public async Task<OutPutModel<object>> UpdateAsync([FromBody] DepartmentUpdate input)
        {
            try
            {
                Department data = await _iDepartment.GetByIdAsync(input.Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("部门信息不存在", null, (int)HttpStatusCode.NotFound);
                if (await _iDepartment.ExistsByCode(input.Code, data.Id))
                    return OutPutMethod<object>.Failure("该部门编号已被使用", null, (int)HttpStatusCode.BadRequest);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<DepartmentUpdate, Department>();
                });
                IMapper mapper = config.CreateMapper();
                Department obj = mapper.Map<Department>(input);
                obj.OperaterId = UserInformation.Id;
                obj.OperateTime = DateTime.Now;
                if (input.ParentId == null)
                    obj.ParentId = Guid.Empty;
                return await _iDepartment.UpdateAsync(obj) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
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
        /// <param name="Id">Id</param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        [Permission("Department_Delete")]
        [TypeFilter(typeof(OperateLogAttribute), Arguments = new object[] { _module, OperateEnum.Delete })]
        public async Task<OutPutModel<object>> DeleteAsync(Guid Id)
        {
            try
            {
                Department data = await _iDepartment.GetByIdAsync(Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("部门信息不存在", null, (int)HttpStatusCode.NotFound);
                if (await _iDepartment.HasChildAsync(data.Id))
                    return OutPutMethod<object>.Failure("存在下级部门，不可删除", null, (int)HttpStatusCode.BadRequest);
                return await _iDepartment.DeleteAsync(data) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
    }
}

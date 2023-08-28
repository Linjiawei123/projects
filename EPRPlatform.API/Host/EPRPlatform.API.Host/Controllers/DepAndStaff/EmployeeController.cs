using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EPRPlatform.API.Models;
using  EPRPlatform.API.Dto.OAModels;
using EPRPlatform.API.Repository;
using EPRPlatform.API.Interfaces;
using System.Collections.Generic;
using System.Net;
using EPRPlatform.API.Dto.PublicModels;
using EPRPlatform.API.Extend;
using EPRPlatform.API.Dto;

namespace EPRPlatform.API.Host.Controllers
{
    /// <summary>
    /// 职工管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [FieldFilter]
    [Authorize]
    public class EmployeeController : BaseController
    {
        private const int _module = 10006;
        private readonly IErrorRepository _iError;
        private readonly IEmployeeRepository _iEmployee;
        private readonly IDepartmentRepository _iDepartment;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iError"></param>
        /// <param name="iEmployee"></param>
        /// <param name="iDepartment"></param>
        public EmployeeController(IErrorRepository iError, IEmployeeRepository iEmployee, IDepartmentRepository iDepartment)
        {
            try
            {
                _iError = iError;
                _iEmployee = iEmployee;
                _iDepartment = iDepartment;
            }
            catch (Exception ex)
            {
                _iError.AddErrorAsync(ex).Wait();
            }
            _iDepartment = iDepartment;
        }

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="Name">名称</param>
        /// <returns></returns>
        [HttpGet("Department")]
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
        /// 获取未绑定职工的用户
        /// </summary>
        /// <returns></returns>
        [HttpGet("NotBindUser")]
        public async Task<OutPutModel<List<UserData>>> GetNotBindUserAsync([FromQuery] Guid? Id)
        {
            try
            {
                List<User> ulist = await _iEmployee.GetNotBindUserAsync(Id);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<User, UserData>()
                    .ForMember(q => q.Name, p => p.MapFrom(w => w.Name + $"({w.Account})"));
                });
                IMapper mapper = config.CreateMapper();
                List<UserData> data = mapper.Map<List<UserData>>(ulist);
                return OutPutMethod<List<UserData>>.Success(data,0);
            }catch(Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<List<UserData>>.Failure();
            }
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="departmentId">部门id</param>
        /// <param name="name">名称</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        [HttpPost("{pageSize}/{pageIndex}")]
        [Permission("Employee_View")]
        public async Task<OutPutModel<PageModel<List<EmployeeSimple>>>> GetPageAsync([FromForm] Guid? departmentId, [FromForm] string name, short pageSize, int pageIndex)
        {
            try
            {
                PageModel<List<EmployeeInfo>> page = await _iEmployee.GetPageAsync(departmentId, name, pageSize, pageIndex);
                PageModel<List<EmployeeSimple>> data = new()
                {
                    RecordCount = page.RecordCount,
                    PageCount = page.PageCount,
                    PageData = new List<EmployeeSimple>()
                };
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<EmployeeInfo, EmployeeSimple>()
                    .ForMember(q => q.Sex, p => p.MapFrom(w => w.Sex ? "男" : "女"))
                    .ForMember(q => q.Birthday, p => p.MapFrom(w => w.Birthday.ToString("yyyy-MM-dd")))
                    .ForMember(q => q.HireDate, p => p.MapFrom(w => w.HireDate.ToString("yyyy-MM-dd")));
                });
                IMapper mapper = config.CreateMapper();
                data.PageData = mapper.Map<List<EmployeeSimple>>(page.PageData);
                return OutPutMethod<PageModel<List<EmployeeSimple>>>.Success(data, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<PageModel<List<EmployeeSimple>>>.Failure();
            }
        }
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [Permission("Employee_View")]
        public async Task<OutPutModel<EmployeeInfo>> GetAsync(Guid Id)
        {
            try
            {
                EmployeeInfo data = await _iEmployee.GetAsync(Id);
                if (data == null)
                    return OutPutMethod<EmployeeInfo>.Failure("职工信息不存在！", null, (int)HttpStatusCode.NotFound);
                return OutPutMethod<EmployeeInfo>.Success(data, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<EmployeeInfo>.Failure();
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input">输入实体</param>
        /// <returns></returns>
        [HttpPost]
        [ModelFilter]
        [Permission("Employee_Add")]
        [TypeFilter(typeof(OperateLogAttribute), Arguments = new object[] { _module, OperateEnum.Add })]
        public async Task<OutPutModel<object>> AddAsync([FromBody] EmployeeAdd input)
        {
            try
            {
                if (await _iEmployee.ExistsCodeAsync(input.Code, null))
                    return OutPutMethod<object>.Failure("职工编号已被使用，请输入新的职工编号", null, (int)HttpStatusCode.BadRequest);
                if (input.UserId.HasValue && await _iEmployee.ExistsUserAsync(input.UserId.Value, null))
                    return OutPutMethod<object>.Failure("该用户已关联其他职工，请重新选择", null, (int)HttpStatusCode.BadRequest);
                var department = await _iDepartment.GetByIdAsync(input.DepartmentId);
                if (department == null)
                    return OutPutMethod<object>.Failure("选择的部门不存在，请重新选择", null, (int)HttpStatusCode.BadRequest);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<EmployeeAdd, Employee>();
                });
                IMapper mapper = config.CreateMapper();
                Employee obj = mapper.Map<Employee>(input);
                obj.Id = Guid.NewGuid();
                obj.OperaterId = UserInformation.Id;
                obj.OperateTime = DateTime.Now;
                return await _iEmployee.AddAsync(obj) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
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
        [Permission("Employee_Update")]
        [TypeFilter(typeof(OperateLogAttribute), Arguments = new object[] { _module, OperateEnum.Update })]
        public async Task<OutPutModel<object>> UpdateAsync([FromBody] EmployeeUpdate input)
        {
            try
            {
                EmployeeInfo data = await _iEmployee.GetAsync(input.Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("职工不存在", null, (int)HttpStatusCode.NotFound);
                if (await _iEmployee.ExistsCodeAsync(input.Code, input.Id))
                    return OutPutMethod<object>.Failure("职工编号已被使用，请输入新的职工编号", null, (int)HttpStatusCode.BadRequest);
                if (input.UserId.HasValue && await _iEmployee.ExistsUserAsync(input.UserId.Value, input.Id))
                    return OutPutMethod<object>.Failure("该用户已关联其他职工，请重新选择", null, (int)HttpStatusCode.BadRequest);
                var department = await _iDepartment.GetByIdAsync(input.DepartmentId);
                if (department == null)
                    return OutPutMethod<object>.Failure("选择的部门不存在，请重新选择", null, (int)HttpStatusCode.BadRequest);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<EmployeeUpdate, Employee>();
                });
                IMapper mapper = config.CreateMapper();
                Employee obj = mapper.Map<Employee>(input);
                obj.OperaterId = UserInformation.Id;
                obj.OperateTime = DateTime.Now;
                return await _iEmployee.UpdateAsync(obj) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
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
        [Permission("Employee_Delete")]
        [TypeFilter(typeof(OperateLogAttribute), Arguments = new object[] { _module, OperateEnum.Delete })]
        public async Task<OutPutModel<object>> DeleteAsync(Guid Id)
        {
            try
            {
                EmployeeInfo data = await _iEmployee.GetAsync(Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("职工不存在", null, (int)HttpStatusCode.NotFound);
                return await _iEmployee.DeleteAsync(data) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
    }
}

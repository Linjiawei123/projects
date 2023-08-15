
namespace MicroService.Dto.OAModels
{
    /// <summary>
    /// 部门输出模型
    /// </summary>
    public class DepartmentSimple
    {
        /// <summary>
        /// 数据id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 上级部门id
        /// </summary>
        public Guid ParentId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 部门主管
        /// </summary>
        public string ManagerId { get; set; }
        /// <summary>
        /// 子部门
        /// </summary>
        public List<DepartmentSimple> Child { get; set; }
    }
}

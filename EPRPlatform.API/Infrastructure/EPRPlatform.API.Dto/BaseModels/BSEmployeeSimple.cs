using EPRPlatform.API.Models.Base;

namespace EPRPlatform.API.Dto.BaseModels
{
    public class BSEmployeeSimple : BSEmployee
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }
    }
}

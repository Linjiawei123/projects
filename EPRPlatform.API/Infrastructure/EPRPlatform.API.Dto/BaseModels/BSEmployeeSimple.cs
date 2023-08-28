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
    public class INSimple
    {
        public List<INEduLevel> eduLevel { get; set; }
        public List<INSex> sex { get; set; }
        public List<BSDepartment> department { get; set; }
    }
}

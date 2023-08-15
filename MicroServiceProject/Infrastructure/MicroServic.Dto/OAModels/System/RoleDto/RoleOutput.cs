using MicroService.Models;

namespace MicroService.Dto.OAModels
{
    /// <summary>
    /// 角色输出模型
    /// </summary>
    public class RoleSimple : Role
    {
        /// <summary>
        /// 角色权限
        /// </summary>
        public new List<Guid> Rights { get; set; }
    }
}

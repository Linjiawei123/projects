using EPRPlatform.API.Models;

namespace EPRPlatform.API.Dto.OAModels
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

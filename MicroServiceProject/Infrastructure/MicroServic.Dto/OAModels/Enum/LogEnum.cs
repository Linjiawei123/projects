using System.ComponentModel;

namespace MicroService.Dto.OAModels
{
    /// <summary>
    /// 操作
    /// </summary>
    public enum OperateEnum
    {
        /// <summary>
        /// 新增
        /// </summary>
        [Description("新增")]
        Add = 10001,
        /// <summary>
        /// 修改
        /// </summary>
        [Description("修改")]
        Update = 10002,
        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete = 10003,
        /// <summary>
        /// 导出
        /// </summary>
        [Description("导出")]
        Export = 10004,
        /// <summary>
        /// 导入
        /// </summary>
        [Description("导入")]
        Import = 10005,
        /// <summary>
        /// 角色用户管理
        /// </summary>
        [Description("角色用户管理")]
        RoleUser = 10006
    }
}

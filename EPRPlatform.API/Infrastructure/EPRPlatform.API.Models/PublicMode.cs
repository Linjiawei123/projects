
namespace EPRPlatform.API.Models
{
    /// <summary>
    /// 公共类型模型
    /// </summary>
    /// <typeparam name="TKey">键类型</typeparam>
    /// <typeparam name="TValue">值类型</typeparam>
    public class PublicModel<TKey,TValue>
    {
        /// <summary>
        /// 键
        /// </summary>
        public TKey Key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public TValue Value { get; set; }
    }

    /// <summary>
    /// 编辑数据模型
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class ListEdit<T>
    {
        /// <summary>
        /// 已存在的数据
        /// </summary>
        public List<T> ListExist { get; set; }
        /// <summary>
        /// 需要添加的数据
        /// </summary>
        public List<T> ListAdd { get; set; }
        /// <summary>
        /// 需要删除的数据
        /// </summary>
        public List<T> ListDelete { get; set; }
    }
}

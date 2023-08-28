
namespace EPRPlatform.API.Models
{
    /// <summary>
    /// 分页数据模型
    /// </summary>
    public class PageModel<T>
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        public int RecordCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// 页面数据
        /// </summary>
        public T PageData { get; set; }
    }
}

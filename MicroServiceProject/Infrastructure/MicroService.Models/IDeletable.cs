namespace MicroService.Models
{
    /// <summary>
	/// 是否已删除
	/// </summary>
    public interface IDeletable
    {
        /// <summary>
        /// 是否已删除
        /// </summary>
        bool IsDeleted { get; set; }
    }
}

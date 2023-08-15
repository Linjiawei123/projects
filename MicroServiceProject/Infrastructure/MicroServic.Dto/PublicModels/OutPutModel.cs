namespace MicroService.Dto.PublicModels
{
    /// <summary>
    /// 输出模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OutPutModel<T>
    {
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 错误类型 1.显示
        /// </summary>
        public int ErrType { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public List<string> Rights { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }
    }
}

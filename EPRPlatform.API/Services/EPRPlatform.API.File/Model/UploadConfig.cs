namespace EPRPlatform.API.File.Model
{
    /// <summary>
    /// 上传配置
    /// </summary>
    public class UploadConfig
    {
        /// <summary>
        /// 上传模块
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// 保存路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 文件地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 最大限制
        /// </summary>
        public int MaxSize { get; set; }
        /// <summary>
        /// 是否缩放图片
        /// </summary>
        public bool Zoom { get; set; }
        /// <summary>
        /// 图片默认缩放宽度
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 图片默认缩放高度
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 是否压缩
        /// </summary>
        public bool Zip { get; set; }
        /// <summary>
        /// 压缩质量 1-100
        /// </summary>
        public int Quality { get; set; }
        /// <summary>
        /// 可上传文件拓展名
        /// </summary>
        public string[] ExtName { get; set; }
    }
    /// <summary>
    /// 上传配置
    /// </summary>
    public class UploadSetting
    {
        /// <summary>
        /// ffmpeg程序路径
        /// </summary>
        public string FFmpegPath { get; set; }
        /// <summary>
        /// 静态路径
        /// </summary>
        public string FileProvider { get; set; }
        /// <summary>
        /// 返回路径
        /// </summary>
        public string RequestPath { get; set; }
        /// <summary>
        /// 上传模型集
        /// </summary>
        public List<UploadConfig> Models { get; set; }
    }
}

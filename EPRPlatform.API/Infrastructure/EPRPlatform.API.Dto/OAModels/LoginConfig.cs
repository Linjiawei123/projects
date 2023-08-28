namespace EPRPlatform.API.Dto.OAModels
{
    /// <summary>
    /// 登录配置
    /// </summary>
    public class LoginConfig
    {
        /// <summary>
        /// 登录请求路径
        /// </summary>
        public string LoginUrl { get; set; }
        /// <summary>
        /// 锁定时间（分钟）
        /// </summary>
        public int LockTime { get; set; }
        /// <summary>
        /// 登录最大次数
        /// </summary>
        public int MaxErrTimes { get; set; }
        /// <summary>
        /// 密码有效天数
        /// </summary>
        public int PasswordValidityDays { get; set; }
        /// <summary>
        /// 是否开启ip限制
        /// </summary>
        public bool LimitOfIP { get; set; }
        /// <summary>
        /// 登录ip范围
        /// </summary>
        public string[] IPRange { get; set; }
    }
}

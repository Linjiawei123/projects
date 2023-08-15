using Microsoft.Extensions.Configuration;

namespace MicroService.OA
{ 
    /// <summary>
    /// 获取配置文件帮助类型
    /// </summary>
    public class AppHelper
    {
        private static IConfiguration _config;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public AppHelper(IConfiguration configuration)
        {
            _config = configuration;
        }

        /// <summary>
        /// 读取指定节点的字符串
        /// </summary>
        /// <param name="sessions"></param>
        /// <returns></returns>
        public string ReadAppSettings(params string[] sessions)
        {
            try
            {
                if (sessions.Any())
                {
                    return _config[string.Join(":", sessions)];
                }
            }
            catch
            {
                return "";
            }
            return "";
        }

        /// <summary>
        /// 读取实体信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <returns></returns>
        public List<T> ReadAppSettings<T>(params string[] session)
        {
            List<T> list = new List<T>();
            _config.Bind(string.Join(":", session), list);
            return list;
        }
    }
}

namespace EPRPlatform.API.Host
{
    /// <summary>
    /// BaseHttpContext
    /// </summary>
    public static class BaseHttpContext
    {
        /// <summary>
        /// 服务集合
        /// </summary>
        public static IServiceCollection ServiceCollection;
        /// <summary>
        /// 当前上下文
        /// </summary>
        public static HttpContext Current
        {
            get
            {
                object factory = ServiceCollection.BuildServiceProvider().GetService(typeof(IHttpContextAccessor));
                HttpContext context = ((HttpContextAccessor)factory).HttpContext;
                return context;
            }
        }

    }
}

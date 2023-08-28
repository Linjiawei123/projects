using EPRPlatform.API.Models;
using EPRPlatform.API.Method;
using EPRPlatform.API.Interfaces;
using EPRPlatform.API.Extend;

namespace EPRPlatform.API.Host.Job
{
    /// <summary>
    /// 定时任务(在redis中取操作日志保存数据库)
    /// </summary>
    public class OperateLogTimeJob : BackgroundService
    {
        private readonly IErrorRepository _iError;
        private readonly ISystemLogRepository _iSystemLog;
        private readonly IRedisInvoker _iRedisInvoker;
        /// <summary>
        /// 构造函数
        /// </summary>
        public OperateLogTimeJob(IErrorRepository iError, ISystemLogRepository iSystemLog, IRedisInvoker iRedisInvoker)
        {
            try
            {
                _iError = iError;
                _iSystemLog = iSystemLog;
                _iRedisInvoker = iRedisInvoker;
            }
            catch (Exception ex)
            {
                _iError.AddErrorAsync(ex).Wait();
            }
        }
        /// <summary>
        /// 定时执行
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await new TaskFactory().StartNew(async () =>
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        try
                        {
                            OperateLog log = await _iRedisInvoker.ListLeftPopAsync<OperateLog>("operateLog");
                            if (log != null)
                                await _iSystemLog.AddOperateLogAsync(log);
                        }
                        catch (Exception ex)
                        {
                            await _iError.AddErrorAsync(ex);
                        }
                        Thread.Sleep(60 * 1000 * 1);
                    }
                }, stoppingToken);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
            }
        }
    }
}

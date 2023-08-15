using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Interfaces
{
    /// <summary>
    /// 错误日志记录接口
    /// </summary>
    public interface IErrorRepository
    {
        /// <summary>
        /// 添加错误日志到数据库
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        Task AddErrorAsync(Exception ex);
    }
}

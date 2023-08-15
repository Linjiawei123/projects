
using MicroService.Models;

namespace MicroService.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUploadRepository
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns></returns>
        Task<bool> AddAsync(UploadFile file);
    }
}

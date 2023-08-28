
using EPRPlatform.API.Models;

namespace EPRPlatform.API.Interfaces
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

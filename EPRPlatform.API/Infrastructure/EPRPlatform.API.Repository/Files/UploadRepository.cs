
using EPRPlatform.API.Models;
using EPRPlatform.API.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace EPRPlatform.API.Repository
{
    public class UploadRepository : DataContext, IUploadRepository
    {
        private readonly DataContext _dbContext;
        private readonly DbSet<UploadFile> _files;
        public UploadRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
            _files = _dbContext.Set<UploadFile>();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(UploadFile file)
        {
            _files.Add(file);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}

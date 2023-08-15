
using MicroService.Models;
using MicroService.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace MicroService.Repository
{
    internal class UploadRepository : DbContext, IUploadRepository
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<UploadFile> _files;
        public UploadRepository(DbContext dbContext)
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

using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Extend
{
    public interface IMongoDBInvoker
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="document"></param>
        /// <returns></returns>
        Task InsertOne<T>(T document);
        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        Task<bool> UpdateOne<T>(Expression<Func<T, bool>> filter, UpdateDefinition<T> update);
        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<bool> DeleteOne<T>(Expression<Func<T, bool>> filter);
        /// <summary>
        /// 查询数据集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<List<T>> Find<T>(Expression<Func<T, bool>> filter);
        /// <summary>
        /// 查询分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<T>> FindPage<T>(Expression<Func<T, bool>> filter, int pageNumber, int pageSize);
        /// <summary>
        /// 查询一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<T> FindOne<T>(Expression<Func<T, bool>> filter);
    }
}

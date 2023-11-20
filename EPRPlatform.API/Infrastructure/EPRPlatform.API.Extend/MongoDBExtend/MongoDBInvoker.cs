using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Extend
{
    public class MongoDBInvoker : IMongoDBInvoker
    {
        private readonly IMongoDatabase _database;

        public MongoDBInvoker(IOptions<MongoDBInvokerOptions> option)
        {
            var client = new MongoClient(option.Value.MongoDBConnectionString);
            _database = client.GetDatabase(option.Value.DataBase);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="document"></param>
        /// <returns></returns>
        public async Task InsertOne<T>(T document)
        {
            IMongoCollection<T> collection = _database.GetCollection<T>(typeof(T).Name);
            await collection.InsertOneAsync(document);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task UpdateOne<T>(Expression<Func<T, bool>> filter, UpdateDefinition<T> update)
        {
            IMongoCollection<T> collection = _database.GetCollection<T>(typeof(T).Name);
            await collection.UpdateOneAsync(filter, update);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task DeleteOne<T>(Expression<Func<T, bool>> filter)
        {
            IMongoCollection<T> collection = _database.GetCollection<T>(typeof(T).Name);
            await collection.DeleteOneAsync(filter);
        }
        /// <summary>
        /// 查询数据集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<List<T>> Find<T>(Expression<Func<T, bool>> filter)
        {
            IMongoCollection<T> collection = _database.GetCollection<T>(typeof(T).Name);
            return await collection.Find(filter).ToListAsync();
        }
        /// <summary>
        /// 查询分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<List<T>> FindPage<T>(Expression<Func<T, bool>> filter, int pageNumber, int pageSize)
        {
            IMongoCollection<T> collection = _database.GetCollection<T>(typeof(T).Name);
            return await collection.Find(filter)
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Limit(pageSize)
                                    .ToListAsync();
        }
        /// <summary>
        /// 查询一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<T> FindOne<T>(Expression<Func<T, bool>> filter)
        {
            IMongoCollection<T> collection = _database.GetCollection<T>(typeof(T).Name);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}

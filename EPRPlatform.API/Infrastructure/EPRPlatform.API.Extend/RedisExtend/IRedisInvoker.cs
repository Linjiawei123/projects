using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Extend
{
    public interface IRedisInvoker
    {
        #region 字符串
        /// <summary>
        /// 设置key，并保存字符串（如果key 已存在，则覆盖）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expried"></param>
        /// <returns></returns>
        bool StringSet(string redisKey, string redisValue, TimeSpan? expried = null);
        /// <summary>
        /// 保存多个key-value
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        bool StringSet(IEnumerable<KeyValuePair<RedisKey, RedisValue>> keyValuePairs);
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        string StringGet(string redisKey);
        /// <summary>
        /// 存储一个对象，该对象会被序列化存储
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expired"></param>
        /// <returns></returns>
        bool StringSet<T>(string redisKey, T redisValue, TimeSpan? expired = null);
        /// <summary>
        /// 获取一个对象(会进行反序列化)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        T StringSet<T>(string redisKey);

        /// <summary>
        /// 保存一个字符串值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expired"></param>
        /// <returns></returns>
        Task<bool> StringSetAsync(string redisKey, string redisValue, TimeSpan? expired = null);
        /// <summary>
        /// 保存一个字符串值
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        Task<bool> StringSetAsync(IEnumerable<KeyValuePair<RedisKey, RedisValue>> keyValuePairs);
        /// <summary>
        /// 获取单个值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<string> StringGetAsync(string redisKey);
        /// <summary>
        /// 存储一个对象（该对象会被序列化保存）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expired"></param>
        /// <returns></returns>
        Task<bool> StringSetAsync<T>(string redisKey, string redisValue, TimeSpan? expired = null);
        /// <summary>
        /// 获取一个对象（反序列化）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<T> StringGetAsync<T>(string redisKey);
        #endregion

        #region Hash操作
        /// <summary>
        /// 判断字段是否在hash中
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        bool HashExist(string redisKey, string hashField);
        /// <summary>
        /// 从hash 中删除字段
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        bool HashDelete(string redisKey, string hashField);
        /// <summary>
        /// 从hash中移除指定字段
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        long HashDelete(string redisKey, IEnumerable<RedisValue> hashField);
        /// <summary>
        /// 在hash中设定值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool HashSet(string redisKey, string hashField, string value);
        /// <summary>
        /// 从Hash 中获取值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        RedisValue HashGet(string redisKey, string hashField);
        /// <summary>
        /// 从Hash 中获取值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        RedisValue[] HashGet(string redisKey, RedisValue[] hashField);
        /// <summary>
        /// 从hash 返回所有的key值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        IEnumerable<RedisValue> HashKeys(string redisKey);
        /// <summary>
        /// 根据key返回hash中的值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        RedisValue[] HashValues(string redisKey);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool HashSet<T>(string redisKey, string hashField, T value);
        /// <summary>
        /// 在hash 中获取值 （反序列化）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        T HashGet<T>(string redisKey, string hashField);
        /// <summary>
        /// 判断字段是否存在hash 中
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        Task<bool> HashExistsAsync(string redisKey, string hashField);
        /// <summary>
        /// 从hash中移除指定字段
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        Task<bool> HashDeleteAsync(string redisKey, string hashField);
        /// <summary>
        /// 从hash中移除指定字段
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        Task<long> HashDeleteAsync(string redisKey, IEnumerable<RedisValue> hashField);
        /// <summary>
        /// 在hash 设置值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> HashSetAsync(string redisKey, string hashField, string value);
        /// <summary>
        /// 在hash 中设定值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashFields"></param>
        /// <returns></returns>
        Task HashSetAsync(string redisKey, IEnumerable<HashEntry> hashFields);
        /// <summary>
        /// 在hash 中设定值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        Task<RedisValue> HashGetAsync(string redisKey, string hashField);
        /// <summary>
        /// 在hash 中获取值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        Task<IEnumerable<RedisValue>> HashGetAsync(string redisKey, RedisValue[] hashField);
        /// <summary>
        /// 从hash返回所有的字段值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<IEnumerable<RedisValue>> HashKeysAsync(string redisKey);
        /// <summary>
        /// 返回hash中所有的值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<IEnumerable<RedisValue>> HashValuesAsync(string redisKey);
        /// <summary>
        /// 在hash 中设定值（序列化）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> HashSetAsync<T>(string redisKey, string hashField, T value);
        /// <summary>
        /// 在hash中获取值（反序列化）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        Task<T> HashGetAsync<T>(string redisKey, string hashField);
        #endregion

        #region 集合操作
        /// <summary>
        /// 将一个泛型List添加到缓存中
        /// </summary>
        /// <typeparam name="T">泛型T</typeparam>
        /// <param name="listkey">Key</param>
        /// <param name="list">list</param>
        /// <returns></returns>
        bool AddList<T>(string listkey, List<T> list);

        /// <summary>
        /// 通过指定Key值获取泛型List
        /// </summary>
        /// <typeparam name="T">泛型T</typeparam>
        /// <param name="listkey">Key</param>
        /// <returns></returns>
        List<T> GetList<T>(string listkey);
        /// <summary>
        /// 是否存在相同的key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool GetKeyExists(string key);
        /// <summary>
        /// 删除指定List中满足条件的元素
        /// </summary>
        /// <param name="listkey">Key</param>
        /// <param name="func">lamdba表达式</param>
        /// <returns></returns>
        bool DelListByLambda<T>(string listkey, Func<T, bool> func);
        /// <summary>
        /// 获取指定List中满足条件的元素
        /// </summary>
        /// <param name="listkey">Key</param>
        /// <param name="func">lamdba表达式</param>
        /// <returns></returns>
        List<T> GetListByLambda<T>(string listkey, Func<T, bool> func);
        /// <summary>
        /// 移除并返回key所对应列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        string ListLeftPop(string redisKey);
        /// <summary>
        /// 移除并返回key所对应列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        string ListRightPop(string redisKey);
        /// <summary>
        /// 移除指定key及key所对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        long ListRemove(string redisKey, string redisValue);
        /// <summary>
        /// 在列表尾部插入值，如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        long ListRightPush(string redisKey, string redisValue);
        /// <summary>
        /// 在列表头部插入值，如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        long ListLeftPush(string redisKey, string redisValue);
        /// <summary>
        /// 返回列表上该键的长度，如果不存在，返回0
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        long ListLength(string redisKey);
        /// <summary>
        /// 返回在该列表上键所对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        IEnumerable<RedisValue> ListRange(string redisKey);
        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        T ListLeftPop<T>(string redisKey);
        /// <summary>
        /// 移除并返回该列表上的最后一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        T ListRightPop<T>(string redisKey);
        /// <summary>
        /// 在列表尾部插入值，如果键不存在，先创建再插入值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        long ListRightPush<T>(string redisKey, T redisValue);
        /// <summary>
        /// 在列表头部插入值，如果键不存在，创建后插入值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        long ListLeftPush<T>(string redisKey, T redisValue);
        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<string> ListLeftPopAsync(string redisKey);
        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<string> ListRightPopAsync(string redisKey);
        /// <summary>
        /// 移除列表指定键上与值相同的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        Task<long> ListRemoveAsync(string redisKey, string redisValue);
        /// <summary>
        /// 在列表尾部差入值，如果键不存在，先创建后插入
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        Task<long> ListRightPushAsync(string redisKey, string redisValue);
        /// <summary>
        /// 在列表头部插入值，如果键不存在，先创建后插入
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        Task<long> ListLeftPushAsync(string redisKey, string redisValue);
        /// <summary>
        /// 返回列表上的长度，如果不存在，返回0
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<long> ListLengthAsync(string redisKey);
        /// <summary>
        /// 返回在列表上键对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<IEnumerable<RedisValue>> ListRangeAsync(string redisKey);
        /// <summary>
        /// 移除并返回存储在key对应列表的第一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<T> ListLeftPopAsync<T>(string redisKey);
        /// <summary>
        /// 移除并返回存储在key 对应列表的最后一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<T> ListRightPopAsync<T>(string redisKey);
        /// <summary>
        /// 在列表尾部插入值，如果值不存在，先创建后写入值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        Task<long> ListRightPushAsync<T>(string redisKey, string redisValue);
        /// <summary>
        /// 在列表头部插入值，如果值不存在，先创建后写入值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        Task<long> ListLeftPushAsync<T>(string redisKey, string redisValue);
        #endregion

        #region sorted set operation
        /// <summary>
        /// sortedset 新增
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        bool SortedSetAdd(string redisKey, string member, double score);
        /// <summary>
        /// 在有序集合中返回指定范围的元素，默认情况下由低到高
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        IEnumerable<RedisValue> SortedSetRangeByRank(string redisKey);
        /// <summary>
        /// 返回有序集合的个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        long SortedSetLength(string redisKey);
        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        bool SortedSetLength(string redisKey, string member);
        /// <summary>
        ///  sorted set Add
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        bool SortedSetAdd<T>(string redisKey, T member, double score);
        /// <summary>
        /// sorted set add
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        Task<bool> SortedSetAddAsync(string redisKey, string member, double score);
        /// <summary>
        /// 在有序集合中返回指定范围的元素，默认情况下由低到高
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<IEnumerable<RedisValue>> SortedSetRangeByRankAsync(string redisKey);
        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<long> SortedSetLengthAsync(string redisKey);
        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        Task<bool> SortedSetRemoveAsync(string redisKey, string member);
        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        Task<bool> SortedSetAddAsync<T>(string redisKey, T member, double score);

        #endregion

        #region key操作
        /// <summary>
        /// 移除指定key
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        bool KeyDelete(string redisKey);
        /// <summary>
        /// 删除指定key
        /// </summary>
        /// <param name="redisKeys"></param>
        /// <returns></returns>
        long KeyDelete(IEnumerable<string> redisKeys);
        /// <summary>
        /// 检验key是否存在
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        bool KeyExists(string redisKey);
        /// <summary>
        /// 重命名key
        /// </summary>
        /// <param name="oldKeyName"></param>
        /// <param name="newKeyName"></param>
        /// <returns></returns>
        bool KeyReName(string oldKeyName, string newKeyName);
        /// <summary>
        /// 设置key 的过期时间
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="expired"></param>
        /// <returns></returns>
        bool KeyExpire(string redisKey, TimeSpan? expired = null);
        /// <summary>
        /// 移除指定的key
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<bool> KeyDeleteAsync(string redisKey);
        /// <summary>
        /// 删除指定的key
        /// </summary>
        /// <param name="redisKeys"></param>
        /// <returns></returns>
        Task<long> KeyDeleteAsync(IEnumerable<string> redisKeys);
        /// <summary>
        /// 检验key 是否存在
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<bool> KeyExistsAsync(string redisKey);
        /// <summary>
        /// 重命名key
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisNewKey"></param>
        /// <returns></returns>
        Task<bool> KeyRenameAsync(string redisKey, string redisNewKey);
        /// <summary>
        /// 设置 key 时间
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="expired"></param>
        /// <returns></returns>
        Task<bool> KeyExpireAsync(string redisKey, TimeSpan? expired);
        #endregion
    }
}

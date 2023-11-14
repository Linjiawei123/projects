using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace EPRPlatform.API.Extend
{
    public class RedisInvoker : IRedisInvoker
    {
        private ConnectionMultiplexer redis = null;
        /// <summary>
        /// 数据库访问对象
        /// </summary>
        private IDatabase _db = null;
        private readonly RedisInvokerOptions _redisOptions;
        public RedisInvoker(IOptions<RedisInvokerOptions> options)
        {
            try
            {
                this._redisOptions = options.Value;
                string RedisConnection = this._redisOptions.RedisConnectionString;
                redis = ConnectionMultiplexer.Connect(RedisConnection);
                _db = redis.GetDatabase();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        #region 字符串
        /// <summary>
        /// 设置key，并保存字符串（如果key 已存在，则覆盖）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expried"></param>
        /// <returns></returns>
        public bool StringSet(string redisKey, string redisValue, TimeSpan? expried = null)
        {
            return _db.StringSet(redisKey, redisValue, expried);
        }
        /// <summary>
        /// 保存多个key-value
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        public bool StringSet(IEnumerable<KeyValuePair<RedisKey, RedisValue>> keyValuePairs)
        {
            keyValuePairs =
                keyValuePairs.Select(x => new KeyValuePair<RedisKey, RedisValue>(x.Key, x.Value));
            return _db.StringSet(keyValuePairs.ToArray());
        }
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public string StringGet(string redisKey)
        {
            return _db.StringGet(redisKey);
        }
        /// <summary>
        /// 存储一个对象，该对象会被序列化存储
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expired"></param>
        /// <returns></returns>
        public bool StringSet<T>(string redisKey, T redisValue, TimeSpan? expired = null)
        {
            string json = JsonConvert.SerializeObject(redisValue);
            return _db.StringSet(redisKey, json, expired);
        }
        /// <summary>
        /// 获取一个对象(会进行反序列化)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public T StringSet<T>(string redisKey)
        {
            string value = _db.StringGet(redisKey);
            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// 保存一个字符串值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expired"></param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync(string redisKey, string redisValue, TimeSpan? expired = null)
        {
            return await _db.StringSetAsync(redisKey, redisValue, expired);
        }
        /// <summary>
        /// 保存一个字符串值
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync(IEnumerable<KeyValuePair<RedisKey, RedisValue>> keyValuePairs)
        {
            keyValuePairs
                = keyValuePairs.Select(x => new KeyValuePair<RedisKey, RedisValue>(x.Key, x.Value));
            return await _db.StringSetAsync(keyValuePairs.ToArray());
        }
        /// <summary>
        /// 获取单个值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<string> StringGetAsync(string redisKey)
        {
            return await _db.StringGetAsync(redisKey);
        }
        /// <summary>
        /// 存储一个对象（该对象会被序列化保存）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expired"></param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync<T>(string redisKey, T redisValue, TimeSpan? expired = null)
        {
            string json = JsonConvert.SerializeObject(redisValue);
            return await _db.StringSetAsync(redisKey, json, expired);
        }
        /// <summary>
        /// 获取一个对象（反序列化）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<T> StringGetAsync<T>(string redisKey)
        {
            string res = await _db.StringGetAsync(redisKey);
            if (string.IsNullOrWhiteSpace(res))
                return default;
            return JsonConvert.DeserializeObject<T>(res);
        }
        #endregion

        #region Hash操作
        /// <summary>
        /// 判断字段是否在hash中
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public bool HashExist(string redisKey, string hashField)
        {
            return _db.HashExists(redisKey, hashField);
        }
        /// <summary>
        /// 从hash 中删除字段
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public bool HashDelete(string redisKey, string hashField)
        {
            return _db.HashDelete(redisKey, hashField);
        }
        /// <summary>
        /// 从hash中移除指定字段
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public long HashDelete(string redisKey, IEnumerable<RedisValue> hashField)
        {
            return _db.HashDelete(redisKey, hashField.ToArray());
        }
        /// <summary>
        /// 在hash中设定值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool HashSet(string redisKey, string hashField, string value)
        {
            return _db.HashSet(redisKey, hashField, value);
        }
        /// <summary>
        /// 从Hash 中获取值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public RedisValue HashGet(string redisKey, string hashField)
        {
            return _db.HashGet(redisKey, hashField);
        }
        /// <summary>
        /// 从Hash 中获取值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public RedisValue[] HashGet(string redisKey, RedisValue[] hashField)
        {
            return _db.HashGet(redisKey, hashField);
        }
        /// <summary>
        /// 从hash 返回所有的key值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public IEnumerable<RedisValue> HashKeys(string redisKey)
        {
            return _db.HashKeys(redisKey);
        }
        /// <summary>
        /// 根据key返回hash中的值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public RedisValue[] HashValues(string redisKey)
        {
            return _db.HashValues(redisKey);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool HashSet<T>(string redisKey, string hashField, T value)
        {
            string json = JsonConvert.SerializeObject(value);
            return _db.HashSet(redisKey, hashField, json);
        }
        /// <summary>
        /// 在hash 中获取值 （反序列化）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public T HashGet<T>(string redisKey, string hashField)
        {
            return JsonConvert.DeserializeObject<T>(_db.HashGet(redisKey, hashField));
        }
        /// <summary>
        /// 判断字段是否存在hash 中
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<bool> HashExistsAsync(string redisKey, string hashField)
        {
            return await _db.HashExistsAsync(redisKey, hashField);
        }
        /// <summary>
        /// 从hash中移除指定字段
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<bool> HashDeleteAsync(string redisKey, string hashField)
        {
            return await _db.HashDeleteAsync(redisKey, hashField);
        }
        /// <summary>
        /// 从hash中移除指定字段
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<long> HashDeleteAsync(string redisKey, IEnumerable<RedisValue> hashField)
        {
            return await _db.HashDeleteAsync(redisKey, hashField.ToArray());
        }
        /// <summary>
        /// 在hash 设置值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> HashSetAsync(string redisKey, string hashField, string value)
        {
            return await _db.HashSetAsync(redisKey, hashField, value);
        }
        /// <summary>
        /// 在hash 中设定值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashFields"></param>
        /// <returns></returns>
        public async Task HashSetAsync(string redisKey, IEnumerable<HashEntry> hashFields)
        {
            await _db.HashSetAsync(redisKey, hashFields.ToArray());
        }
        /// <summary>
        /// 在hash 中设定值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<RedisValue> HashGetAsync(string redisKey, string hashField)
        {
            return await _db.HashGetAsync(redisKey, hashField);
        }
        /// <summary>
        /// 在hash 中获取值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RedisValue>> HashGetAsync(string redisKey, RedisValue[] hashField)
        {
            return await _db.HashGetAsync(redisKey, hashField);
        }
        /// <summary>
        /// 从hash返回所有的字段值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RedisValue>> HashKeysAsync(string redisKey)
        {
            return await _db.HashKeysAsync(redisKey);
        }
        /// <summary>
        /// 返回hash中所有的值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RedisValue>> HashValuesAsync(string redisKey)
        {
            return await _db.HashValuesAsync(redisKey);
        }
        /// <summary>
        /// 在hash 中设定值（序列化）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> HashSetAsync<T>(string redisKey, string hashField, T value)
        {
            string json = JsonConvert.SerializeObject(value);
            return await _db.HashSetAsync(redisKey, hashField, json);
        }
        /// <summary>
        /// 在hash中获取值（反序列化）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<T> HashGetAsync<T>(string redisKey, string hashField)
        {
            return JsonConvert.DeserializeObject<T>(await _db.HashGetAsync(redisKey, hashField));
        }
        #endregion

        #region 集合操作
        /// <summary>
        /// 将一个泛型List添加到缓存中
        /// </summary>
        /// <typeparam name="T">泛型T</typeparam>
        /// <param name="listkey">Key</param>
        /// <param name="list">list</param>
        /// <returns></returns>
        public bool AddList<T>(string listkey, List<T> list)
        {
            if (_db == null)
            {
                return false;
            }
            var value = JsonConvert.SerializeObject(list);
            return _db.StringSet(listkey, value);

        }

        /// <summary>
        /// 通过指定Key值获取泛型List
        /// </summary>
        /// <typeparam name="T">泛型T</typeparam>
        /// <param name="listkey">Key</param>
        /// <returns></returns>
        public List<T> GetList<T>(string listkey)
        {
            if (_db == null)
            {
                return new List<T>();
            }
            if (_db.KeyExists(listkey))
            {
                var value = _db.StringGet(listkey);
                if (!string.IsNullOrEmpty(value))
                {
                    var list = JsonConvert.DeserializeObject<List<T>>(value);
                    return list;
                }
                else
                    return new List<T>();
            }
            else
                return new List<T>();
        }
        /// <summary>
        /// 是否存在相同的key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool GetKeyExists(string key)
        {
            if (_db == null)
                return false;
            if (_db.KeyExists(key))
                return true;
            else
                return false;
        }
        /// <summary>
        /// 删除指定List中满足条件的元素
        /// </summary>
        /// <param name="listkey">Key</param>
        /// <param name="func">lamdba表达式</param>
        /// <returns></returns>
        public bool DelListByLambda<T>(string listkey, Func<T, bool> func)
        {
            if (_db == null)
                return false;
            if (_db.KeyExists(listkey))
            {
                var value = _db.StringGet(listkey);
                if (!string.IsNullOrEmpty(value))
                {
                    var list = JsonConvert.DeserializeObject<List<T>>(value);
                    if (list.Count > 0)
                    {
                        list = list.SkipWhile<T>(func).ToList();
                        value = JsonConvert.SerializeObject(list);
                        return _db.StringSet(listkey, value);
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }
        /// <summary>
        /// 获取指定List中满足条件的元素
        /// </summary>
        /// <param name="listkey">Key</param>
        /// <param name="func">lamdba表达式</param>
        /// <returns></returns>
        public List<T> GetListByLambda<T>(string listkey, Func<T, bool> func)
        {
            if (_db == null)
                return new List<T>();
            if (_db.KeyExists(listkey))
            {
                var value = _db.StringGet(listkey);
                if (!string.IsNullOrEmpty(value))
                {
                    var list = JsonConvert.DeserializeObject<List<T>>(value);
                    if (list.Count > 0)
                    {
                        list = list.Where(func).ToList();
                        return list;
                    }
                    else
                        return new List<T>();
                }
                else
                    return new List<T>();
            }
            else
                return new List<T>();
        }
        /// <summary>
        /// 移除并返回key所对应列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public string ListLeftPop(string redisKey)
        {
            return _db.ListLeftPop(redisKey);
        }
        /// <summary>
        /// 移除并返回key所对应列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public string ListRightPop(string redisKey)
        {
            return _db.ListRightPop(redisKey);
        }
        /// <summary>
        /// 移除指定key及key所对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public long ListRemove(string redisKey, string redisValue)
        {
            return _db.ListRemove(redisKey, redisValue);
        }
        /// <summary>
        /// 在列表尾部插入值，如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public long ListRightPush(string redisKey, string redisValue)
        {
            return _db.ListRightPush(redisKey, redisValue);
        }
        /// <summary>
        /// 在列表头部插入值，如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public long ListLeftPush(string redisKey, string redisValue)
        {
            return _db.ListLeftPush(redisKey, redisValue);
        }
        /// <summary>
        /// 返回列表上该键的长度，如果不存在，返回0
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public long ListLength(string redisKey)
        {
            return _db.ListLength(redisKey);
        }
        /// <summary>
        /// 返回在该列表上键所对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public IEnumerable<RedisValue> ListRange(string redisKey)
        {
            return _db.ListRange(redisKey);
        }
        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public T ListLeftPop<T>(string redisKey)
        {
            return JsonConvert.DeserializeObject<T>(_db.ListLeftPop(redisKey));
        }
        /// <summary>
        /// 移除并返回该列表上的最后一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public T ListRightPop<T>(string redisKey)
        {
            return JsonConvert.DeserializeObject<T>(_db.ListRightPop(redisKey));
        }
        /// <summary>
        /// 在列表尾部插入值，如果键不存在，先创建再插入值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public long ListRightPush<T>(string redisKey, T redisValue)
        {
            return _db.ListRightPush(redisKey, JsonConvert.SerializeObject(redisValue));
        }
        /// <summary>
        /// 在列表头部插入值，如果键不存在，创建后插入值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public long ListLeftPush<T>(string redisKey, T redisValue)
        {
            return _db.ListRightPush(redisKey, JsonConvert.SerializeObject(redisValue));
        }
        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<string> ListLeftPopAsync(string redisKey)
        {
            return await _db.ListLeftPopAsync(redisKey);
        }
        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<string> ListRightPopAsync(string redisKey)
        {
            return await _db.ListRightPopAsync(redisKey);
        }
        /// <summary>
        /// 移除列表指定键上与值相同的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListRemoveAsync(string redisKey, string redisValue)
        {
            return await _db.ListRemoveAsync(redisKey, redisValue);
        }
        /// <summary>
        /// 在列表尾部差入值，如果键不存在，先创建后插入
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListRightPushAsync(string redisKey, string redisValue)
        {
            return await ListRightPushAsync(redisKey, redisValue);
        }
        /// <summary>
        /// 在列表头部插入值，如果键不存在，先创建后插入
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListLeftPushAsync(string redisKey, string redisValue)
        {
            return await _db.ListLeftPushAsync(redisKey, redisValue);
        }
        /// <summary>
        /// 返回列表上的长度，如果不存在，返回0
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<long> ListLengthAsync(string redisKey)
        {
            return await _db.ListLengthAsync(redisKey);
        }
        /// <summary>
        /// 返回在列表上键对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RedisValue>> ListRangeAsync(string redisKey)
        {
            return await _db.ListRangeAsync(redisKey);
        }
        /// <summary>
        /// 移除并返回存储在key对应列表的第一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<T> ListLeftPopAsync<T>(string redisKey)
        {
            var v = await _db.ListLeftPopAsync(redisKey);
            if (!v.HasValue)
                return default(T);
            return JsonConvert.DeserializeObject<T>(v);
        }
        /// <summary>
        /// 移除并返回存储在key 对应列表的最后一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<T> ListRightPopAsync<T>(string redisKey)
        {
            return JsonConvert.DeserializeObject<T>(await _db.ListRightPopAsync(redisKey));
        }
        /// <summary>
        /// 在列表尾部插入值，如果值不存在，先创建后写入值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListRightPushAsync<T>(string redisKey, string redisValue)
        {
            return await _db.ListRightPushAsync(redisKey, JsonConvert.SerializeObject(redisValue));
        }
        /// <summary>
        /// 在列表头部插入值，如果值不存在，先创建后写入值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListLeftPushAsync<T>(string redisKey, string redisValue)
        {
            return await _db.ListLeftPushAsync(redisKey, JsonConvert.SerializeObject(redisValue));
        }
        #endregion

        #region sorted set operation
        /// <summary>
        /// sortedset 新增
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public bool SortedSetAdd(string redisKey, string member, double score)
        {
            return _db.SortedSetAdd(redisKey, member, score);
        }
        /// <summary>
        /// 在有序集合中返回指定范围的元素，默认情况下由低到高
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public IEnumerable<RedisValue> SortedSetRangeByRank(string redisKey)
        {
            return _db.SortedSetRangeByRank(redisKey);
        }
        /// <summary>
        /// 返回有序集合的个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public long SortedSetLength(string redisKey)
        {
            return _db.SortedSetLength(redisKey);
        }
        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public bool SortedSetLength(string redisKey, string member)
        {
            return _db.SortedSetRemove(redisKey, member);
        }
        /// <summary>
        ///  sorted set Add
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public bool SortedSetAdd<T>(string redisKey, T member, double score)
        {
            string json = JsonConvert.SerializeObject(member);
            return _db.SortedSetAdd(redisKey, json, score);
        }
        /// <summary>
        /// sorted set add
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public async Task<bool> SortedSetAddAsync(string redisKey, string member, double score)
        {
            return await _db.SortedSetAddAsync(redisKey, member, score);
        }
        /// <summary>
        /// 在有序集合中返回指定范围的元素，默认情况下由低到高
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RedisValue>> SortedSetRangeByRankAsync(string redisKey)
        {
            return await _db.SortedSetRangeByRankAsync(redisKey);
        }
        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<long> SortedSetLengthAsync(string redisKey)
        {
            return await _db.SortedSetLengthAsync(redisKey);
        }
        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public async Task<bool> SortedSetRemoveAsync(string redisKey, string member)
        {
            return await _db.SortedSetRemoveAsync(redisKey, member);
        }
        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public async Task<bool> SortedSetAddAsync<T>(string redisKey, T member, double score)
        {
            string json = JsonConvert.SerializeObject(member);
            return await _db.SortedSetAddAsync(redisKey, json, score);
        }

        #endregion

        #region key操作
        /// <summary>
        /// 移除指定key
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public bool KeyDelete(string redisKey)
        {
            if (_db.KeyExists(redisKey))
                return _db.KeyDelete(redisKey);
            else
                return true;
        }
        /// <summary>
        /// 删除指定key
        /// </summary>
        /// <param name="redisKeys"></param>
        /// <returns></returns>
        public long KeyDelete(IEnumerable<string> redisKeys)
        {
            var keys = redisKeys.Select(x => (RedisKey)x);
            return _db.KeyDelete(keys.ToArray());
        }
        /// <summary>
        /// 检验key是否存在
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public bool KeyExists(string redisKey)
        {
            return _db.KeyExists(redisKey);
        }
        /// <summary>
        /// 重命名key
        /// </summary>
        /// <param name="oldKeyName"></param>
        /// <param name="newKeyName"></param>
        /// <returns></returns>
        public bool KeyReName(string oldKeyName, string newKeyName)
        {
            return _db.KeyRename(oldKeyName, newKeyName);
        }
        /// <summary>
        /// 设置key 的过期时间
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="expired"></param>
        /// <returns></returns>
        public bool KeyExpire(string redisKey, TimeSpan? expired = null)
        {
            return _db.KeyExpire(redisKey, expired);
        }
        /// <summary>
        /// 移除指定的key
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<bool> KeyDeleteAsync(string redisKey)
        {
            if (await _db.KeyExistsAsync(redisKey))
                return await _db.KeyDeleteAsync(redisKey);
            else
                return true;
        }
        /// <summary>
        /// 删除指定的key
        /// </summary>
        /// <param name="redisKeys"></param>
        /// <returns></returns>
        public async Task<long> KeyDeleteAsync(IEnumerable<string> redisKeys)
        {
            var keys = redisKeys.Select(x => (RedisKey)x);
            return await _db.KeyDeleteAsync(keys.ToArray());
        }
        /// <summary>
        /// 检验key 是否存在
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<bool> KeyExistsAsync(string redisKey)
        {
            return await _db.KeyExistsAsync(redisKey);
        }
        /// <summary>
        /// 重命名key
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisNewKey"></param>
        /// <returns></returns>
        public async Task<bool> KeyRenameAsync(string redisKey, string redisNewKey)
        {
            return await _db.KeyRenameAsync(redisKey, redisNewKey);
        }
        /// <summary>
        /// 设置 key 时间
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="expired"></param>
        /// <returns></returns>
        public async Task<bool> KeyExpireAsync(string redisKey, TimeSpan? expired)
        {
            return await _db.KeyExpireAsync(redisKey, expired);
        }
        #endregion
    }
}

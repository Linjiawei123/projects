using MicroService.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Method
{
    /// <summary>
    /// 公共方法
    /// </summary>
    public static class PublicMethods
    {
        /// <summary>
        /// 获取总页数
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="recordCount">记录总数</param>
        /// <returns></returns>
        public static int GetPageCount(short pageSize, int recordCount)
        {
            return (int)Math.Ceiling((double)recordCount / pageSize);
        }
        /// <summary>
        /// 根据Key值获取Value（没有返回null）
        /// </summary>
        /// <typeparam name="T1">类型1</typeparam>
        /// <typeparam name="T2">类型2</typeparam>
        /// <param name="list">类型集合</param>
        /// <param name="key">key值</param>
        /// <returns></returns>
        public static object GetValueByKey<T1, T2>(List<PublicModel<T1, T2>> list, T1 key)
        {
            PublicModel<T1, T2> result = list.FirstOrDefault(w => w.Key.Equals(key));
            if (result == null)
                return null;

            return result.Value;
        }
        /// <summary>
        /// 根据Value值获取Key（没有返回null或空字符串）
        /// </summary>
        /// <typeparam name="T1">类型1</typeparam>
        /// <typeparam name="T2">类型2</typeparam>
        /// <param name="list">类型集合</param>
        /// <param name="value">value值</param>
        /// <returns></returns>
        public static object GetKeyByValue<T1, T2>(List<PublicModel<T1, T2>> list, T2 value)
        {
            PublicModel<T1, T2> result = list.FirstOrDefault(w => w.Value.Equals(value));
            if (result == null)
            {
                if (typeof(T1).Equals(typeof(string)))
                    return "";
                else
                    return null;
            }
            return result.Key;
        }
        /// <summary>
        /// 根据Key集合值获取Value（没有返回null）
        /// </summary>
        /// <typeparam name="T1">类型1</typeparam>
        /// <typeparam name="T2">类型2</typeparam>
        /// <param name="list">类型集合</param>
        /// <param name="keyStr">key值</param>
        /// <param name="separator">返回字符串的分隔符（默认为中文逗号）</param>
        /// <returns></returns>
        public static object GetValueByKeyList<T1, T2>(List<PublicModel<T1, T2>> list, string keyStr, char separator = '，')
        {
            List<T1> keyList = keyStr.Split(separator).ToList() as List<T1>;
            T2[] result = list.Where(w => keyList.Contains(w.Key)).Select(s => s.Value).ToArray();
            if (result == null && result.Length < 1)
                return null;

            return string.Join(separator, result);
        }
        /// <summary>
        /// 根据Value集合值获取Key（没有返回null或空字符串）
        /// </summary>
        /// <typeparam name="T1">类型1</typeparam>
        /// <typeparam name="T2">类型2</typeparam>
        /// <param name="list">类型集合</param>
        /// <param name="valueStr">value值</param>
        /// <param name="separator">返回字符串的分隔符（默认为中文逗号）</param>
        /// <returns></returns>
        public static object GetKeyByValueList<T1, T2>(List<PublicModel<T1, T2>> list, string valueStr, char separator = '，')
        {
            List<T2> keyList = valueStr.Split(separator).ToList() as List<T2>;
            T2[] result = list.Where(w => keyList.Contains(w.Value)).Select(s => s.Value).ToArray();
            if (result == null && result.Length < 1)
            {
                if (typeof(T1).Equals(typeof(string)))
                    return "";
                else
                    return null;
            }
            return string.Join(separator, result);
        }
        /// <summary>
        /// 根据Key值获取Value（没有返回空字符串）
        /// </summary>
        /// <param name="list">类型集合</param>
        /// <param name="key">key值</param>
        /// <returns></returns>
        public static string GetValueByKey(List<PublicModel<short, string>> list, short key)
        {
            PublicModel<short, string> result = list.FirstOrDefault(w => w.Key == key);
            if (result == null)
                return "";

            return result.Value;
        }
        /// <summary>
        /// 根据Value值获取Key
        /// </summary>
        /// <param name="list">类型集合</param>
        /// <param name="value">value值</param>
        /// <returns></returns>
        public static short? GetKeyByValue(List<PublicModel<short, string>> list, string value)
        {
            PublicModel<short, string> result = list.FirstOrDefault(w => w.Value == value);
            if (result == null)
                return null;
            return result.Key;
        }

        /// <summary>
        /// 获取编辑数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="listOld">旧数据</param>
        /// <param name="listNew">新数据</param>
        /// <returns></returns>
        public static ListEdit<T> GetListEdit<T>(List<T> listOld, List<T> listNew)
        {
            ListEdit<T> list = new()
            {
                ListExist = new(),
                ListAdd = new(),
                ListDelete = new()
            };

            listOld ??= new();
            listNew ??= new();
            //如果旧数据为空数组，则全部添加
            if (listOld.Count == 0)
            {
                list.ListAdd = listNew;
                return list;
            }
            //如果新数据为空数组，则全部删除
            if (listNew.Count == 0)
            {
                list.ListDelete = listOld;
                return list;
            }

            list.ListExist = listOld.FindAll(w => listNew.Contains(w));

            if (list.ListExist.Count == 0)
            {
                list.ListAdd = listNew;
                list.ListDelete = listOld;
            }
            else
            {
                list.ListAdd = listNew.FindAll(w => !list.ListExist.Contains(w));
                list.ListDelete = listOld.FindAll(w => !list.ListExist.Contains(w));
            }

            return list;
        }

        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum enumValue)
        {
            string value = enumValue.ToString();
            FieldInfo field = enumValue.GetType().GetField(value);
            object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);    //获取描述属性
            if (objs == null || objs.Length == 0)    //当描述属性没有时，直接返回名称
                return value;
            DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
            return descriptionAttribute.Description;
        }
        /// <summary>
        /// 根据枚举类型值获取对应的注释属性
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="value">枚举类型</param>
        /// <returns>对应的注释属性</returns>
        public static string GetEnumDescription<T>(object value)
        {
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("enumItem requires a Enum ");
            }

            var name = Enum.GetName(enumType, Convert.ToInt32(value));
            if (name == null)
                return string.Empty;

            object[] objs = enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (objs == null || objs.Length == 0)
            {
                return string.Empty;
            }
            else
            {
                DescriptionAttribute attr = objs[0] as DescriptionAttribute;
                return attr.Description;
            }
        }
        /// <summary>
        /// 获取模型错误
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static List<PublicModel<string, string>> AllModelStateErrors(ModelStateDictionary modelState)
        {
            var result = new List<PublicModel<string, string>>();
            //找到出错的字段以及出错信息
            var errorFieldsAndMsgs = modelState.Where(m => m.Value.Errors.Any())
                .Select(x => new { x.Key, x.Value.Errors });
            foreach (var item in errorFieldsAndMsgs)
            {
                //获取键
                var fieldKey = item.Key;
                //获取键对应的错误信息
                var fieldErrors = item.Errors
                    .Select(e => new PublicModel<string, string>() { Key = fieldKey, Value = e.ErrorMessage });
                result.AddRange(fieldErrors);
            }
            return result;
        }
    }
}

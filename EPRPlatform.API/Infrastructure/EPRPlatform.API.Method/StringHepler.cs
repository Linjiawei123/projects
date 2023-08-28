using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EPRPlatform.API.Method
{
    /// <summary>
    /// 字符串帮助类
    /// </summary>
    public static class StringHepler
    {
        #region 字符串处理

        /// <summary>
        /// 填充字符串
        /// </summary>
        /// <param name="format">占位字符串</param>
        /// <param name="args">占位值</param>
        /// <returns>新字符串</returns>
        public static string Fmt(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
        /// <summary>
        /// 追加字符串
        /// </summary>
        /// <param name="value">当前值</param>
        /// <param name="appendValue">追加值</param>
        /// <returns></returns>
        public static string Append(this string value, string appendValue)
        {
            return value += appendValue;
        }
        /// <summary>
        /// 序列化对象为Json字符串
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="value">转换对象</param>
        /// <returns>json字符串</returns>
        public static string ToJson<T>(this T value)
        {
            return SerializationHelper.SerializeToJson(value);
        }
        /// <summary>
        /// 序列化Json字符串为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="value">json字符串</param>
        /// <returns>对象</returns>
        public static T FromJson<T>(this string value)
        {
            return SerializationHelper.DeserializeFromJson<T>(value);
        }

        /// <summary>
        /// 用指定字符串作为分隔符切割字符串
        /// </summary>
        public static List<string> ToList(this string value, string[] split)
        {
            string[] splitArray = value.Split(split, StringSplitOptions.RemoveEmptyEntries);
            return splitArray.ToList();
        }
        /// <summary>
        /// 获取安全字符串
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>除了最前最后字符外,中间统一用***代替</returns>
        public static string GetSecurityString(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return "";
            else if (str.Length == 1)
                return str;

            string strOutput;
            if (str.Length == 2)
                strOutput = string.Concat(str.AsSpan(0, 1), "**");
            else
                strOutput = string.Concat(str.AsSpan(0, 1), "**", str.AsSpan(str.Length - 1, 1));

            return strOutput;
        }
        #endregion

        #region 字符串校验
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>结果</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>结果</returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
        /// <summary>
        /// 是否含有中文
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>结果</returns>
        public static bool HasChinese(this string value)
        {
            return Regex.IsMatch(value, @"[\u4e00-\u9fa5]");
        }
        /// <summary>
        /// 是否全中文字符
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>结果</returns>
        public static bool IsChinese(this string value)
        {
            return Regex.IsMatch(value, @"^[\u4e00-\u9fa5]+$");
        }
        /// <summary>
        /// 是否邮箱
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>结果</returns>
        public static bool IsEmail(this string value)
        {
            return Regex.IsMatch(value, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }
        /// <summary>
        /// 是否手机号
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>结果</returns>
        public static bool IsMobile(this string value)
        {
            return new Regex("^1[0-9]{10}$").IsMatch(value);
        }
        /// <summary>
        /// 是否电话
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>结果</returns>
        public static bool IsPhone(this string value)
        {
            return new Regex(@"^(\d{3,4}-?)?\d{7,8}$").IsMatch(value);
        }
        /// <summary>
        /// 是否IP4地址
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>结果</returns>
        public static bool IsIPV4(this string value)
        {
            string[] IPs = value.Split('.');

            for (int i = 0; i < IPs.Length; i++)
            {
                if (!Regex.IsMatch(IPs[i], @"^\d+$"))
                {
                    return false;
                }
                if (Convert.ToUInt16(IPs[i]) > 255)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 是否身份证
        /// </summary>
        /// <param name="id">字符串值</param>
        /// <returns>结果</returns>
        public static bool IsIDCard(this string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;
            if (id.Length == 18)
                return IsIDCard18(id);
            else if (id.Length == 15)
                return IsIDCard15(id);
            else
                return false;
        }
        /// <summary>
        /// 是否15位身份证
        /// </summary>
        /// <param name="Id">字符串值</param>
        /// <returns>结果</returns>
        static bool IsIDCard15(this string Id)
        {
            if (long.TryParse(Id, out long n) == false || n < Math.Pow(10, 14))
                return false;//数字验证

            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (!address.Contains(Id.Remove(2), StringComparison.CurrentCulture))
                return false;//省份验证

            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
#pragma warning disable IDE0059 // 不需要赋值
            if (!DateTime.TryParse(birth, out DateTime time))
                return false;//生日验证
#pragma warning restore IDE0059 // 不需要赋值

            return true;//符合15位身份证标准
        }
        /// <summary>
        /// 是否18位身份证
        /// </summary>
        /// <param name="Id">字符串值</param>
        /// <returns>结果</returns>
        static bool IsIDCard18(this string Id)
        {
#pragma warning disable IDE0059 // 不需要赋值
            if (long.TryParse(Id.Remove(17), out long n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
                return false;//数字验证
#pragma warning restore IDE0059 // 不需要赋值

            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (!address.Contains(Id.Remove(2), StringComparison.CurrentCulture))
                return false;//省份验证

            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
#pragma warning disable IDE0059 // 不需要赋值
            if (DateTime.TryParse(birth, out DateTime time) == false)
                return false;//生日验证
#pragma warning restore IDE0059 // 不需要赋值

            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());

            Math.DivRem(sum, 11, out int y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
                return false;//校验码验证

            return true;//符合GB11643-1999标准
        }
        /// <summary>
        /// 是否日期
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>结果</returns>
        public static bool IsDate(this string value)
        {
            return new Regex(@"(\d{4})-(\d{1,2})-(\d{1,2})").IsMatch(value);
        }
        /// <summary>
        /// 是否数字
        /// </summary>
        /// <param name="numericStr">字符串值</param>
        /// <returns>结果</returns>
        public static bool IsNumeric(this string numericStr)
        {
            return new Regex(@"^[-]?[0-9]+(\.[0-9]+)?$").IsMatch(numericStr);
        }

        /// <summary>
        /// 是否小写
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>结果</returns>
        public static bool IsLetter(this string value)
        {
            return new Regex(@"^[A-Za-z]+$").IsMatch(value);
        }

        /// <summary>
        /// 是否压缩
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>结果</returns>
        public static bool IsZipCode(this string value)
        {
            return new Regex(@"^\d{6}$").IsMatch(value);
        }

        /// <summary>
        /// 是否包含
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <param name="list">内容集合</param>
        /// <returns>结果</returns>
        public static bool Contains(this string value, IEnumerable<string> list)
        {
            return list.Any(value.Contains);
        }

        #endregion


        /// <summary>
        /// 获取 url链接 参数名对应的值，需要特定格式
        /// </summary>
        /// <param name="url">url链接</param>
        /// <param name="parameter">参数名</param>
        /// <returns>对应参数值</returns>
        public static string GetUrlParameterValue(string url, string parameter)
        {
            var index = url.IndexOf("?");
            if (index > -1)
            {
                index++;
                var targetUrl = url.Substring(index, url.Length - index);
                string[] Param = targetUrl.Split('&');
                foreach (var parm in Param)
                {
                    var values = parm.Split('=');
                    if (values[0].ToLower().Equals(parameter.ToLower()))
                        return values[1];
                }
            }
            return null;
        }

        #region 随机字符串
        private static char[] constant = {
            '0','1','2','3','4','5','6','7','8','9',
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
        };
        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="Length">字符串长度</param>
        /// <returns></returns>
        public static string GenerateRandomNumber(int Length)
        {
            StringBuilder newRandom = new StringBuilder(62);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(62)]);
            }
            return newRandom.ToString();
        }
        #endregion
    }
}

using System.Security.Cryptography;
using System.Text;

namespace EPRPlatform.API.Method
{
    /// <summary>
    /// 帮助类：MD5加密
    /// </summary>
    public static class Md5Helper
    {
        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>MD5加密后的字符串（32位）</returns>
        public static string Encrypt32(string input, Encoding encoding = null)
        {
            StringBuilder sb = new StringBuilder();
            using (MD5 md5 = MD5.Create())
            {
                if (encoding == null)
                {
                    encoding = Encoding.UTF8;
                }
                byte[] buff = md5.ComputeHash(encoding.GetBytes(input));
                foreach (byte t in buff)
                {
                    sb.AppendFormat("{0:x2}", t);
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>MD5加密后的字符串（16位）</returns>
        public static string Encrypt16(string input, Encoding encoding = null)
        {
            StringBuilder sb = new StringBuilder();
            using (MD5 md5 = MD5.Create())
            {
                if (encoding == null)
                {
                    encoding = Encoding.UTF8;
                }
                byte[] buff = md5.ComputeHash(encoding.GetBytes(input));
                foreach (byte t in buff)
                {
                    sb.AppendFormat("{0:x2}", t);
                }
            }
            return sb.ToString();
        }
    }
}

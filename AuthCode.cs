using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

namespace WXShare
{
    /// <summary>
    /// 网易云信短信验证码接口
    /// </summary>
    public class AuthCode
    {
        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="content">要加密的内容</param>
        /// <returns></returns>
        public static string SHA1(string content)
        {
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] bytes_in = Encoding.UTF8.GetBytes(content);
                byte[] bytes_out = sha1.ComputeHash(bytes_in);
                sha1.Dispose();
                string result = BitConverter.ToString(bytes_out);
                result = result.Replace("-", "");
                return result.ToLower();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        /// 获得一个由数字大小写字母组成的随机字符串
        /// </summary>
        /// <param name="length">字符串长度</param>
        /// <returns></returns>
        public static string GetRandomString(int length)
        {
            byte[] b = new byte[4];
            new RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null;
            var str = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }
        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <returns></returns>
        public static bool SendAuthCode(String phone)
        {
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            try
            {
                // http请求
                request = WebRequest.Create(
                    "https://api.netease.im/sms/sendcode.action") as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                var appSecret = "db9cc835b811";
                request.Headers.Add("AppKey", "29b612d279f0c2691ac683251f3b6a8c");
                var nonce = GetRandomString(16);
                request.Headers.Add("Nonce", nonce);
                var curTime = WXManage.DateTimeToTimeStamp(DateTime.Now).ToString();
                request.Headers.Add("CurTime", curTime);
                // SHA1(AppSecret + Nonce + CurTime)
                request.Headers.Add("CheckSum", SHA1(appSecret + nonce + curTime));
                var postData = "";
                postData += "&mobile=" + phone;
                var data = Encoding.ASCII.GetBytes(postData);
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                // 返回结果
                response = request.GetResponse() as HttpWebResponse;
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, Encoding.UTF8);
                string content = sr.ReadToEnd();
                // 读取返回状态
                JObject jsonObject = (JObject)JsonConvert.DeserializeObject(content);
                if(jsonObject["code"].ToString() == "200")
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 短信验证码是否正确
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="code">短信验证码</param>
        /// <returns></returns>
        public static bool CheckAuthCode(String phone, String code)
        {
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            try
            {
                // http请求
                request = WebRequest.Create(
                    "https://api.netease.im/sms/verifycode.action") as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                var appSecret = "db9cc835b811";
                request.Headers.Add("AppKey", "29b612d279f0c2691ac683251f3b6a8c");
                var nonce = GetRandomString(16);
                request.Headers.Add("Nonce", nonce);
                var curTime = WXManage.DateTimeToTimeStamp(DateTime.Now).ToString();
                request.Headers.Add("CurTime", curTime);
                // SHA1(AppSecret + Nonce + CurTime)
                request.Headers.Add("CheckSum", SHA1(appSecret + nonce + curTime));
                var postData = "mobile=" + phone + "&code=" + code;
                var data = Encoding.ASCII.GetBytes(postData);
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                // 返回结果
                response = request.GetResponse() as HttpWebResponse;
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, Encoding.UTF8);
                string content = sr.ReadToEnd();
                // 读取返回状态
                JObject jsonObject = (JObject)JsonConvert.DeserializeObject(content);
                if (jsonObject["code"].ToString() == "200")
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
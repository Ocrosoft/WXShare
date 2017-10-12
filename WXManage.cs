using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WXShare
{
    /// <summary>
    /// 微信接口管理
    /// </summary>
    public class WXManage
    {
        // 公众号appID
        private static String appID = "wxe47cb091fd410b46";
        // 公众号appsecret
        private static String appsecret = "0a27cc9b2348b761de483e14dccd7cef";
        // access_token
        private static String access_token = String.Empty;
        // 下次要刷新的时间
        private static Int64 timeStamp;
        /// <summary>
        /// DateTime转UNIX时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static Int64 DateTimeToTimeStamp(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        /// <summary>
        /// 刷新access_token
        /// </summary>
        /// <returns></returns>
        public static String RefreshAccessToken()
        {
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            try
            {
                // http请求
                request = WebRequest.Create(
                    "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" +
                    appID + "&secret=" + appsecret) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Headers.Add("Authorization", "Basic YWRtaW46YWRtaW4=");
                // 返回结果
                response = request.GetResponse() as HttpWebResponse;
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, Encoding.UTF8);
                string content = sr.ReadToEnd();
                // 获取access_token，计算过期时间
                JObject jsonObject = (JObject)JsonConvert.DeserializeObject(content);
                access_token = jsonObject["access_token"].ToString();
                timeStamp = DateTimeToTimeStamp(DateTime.Now) + 
                    Convert.ToInt64(jsonObject["expires_in"].ToString());
                return access_token;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
       /// <summary>
       /// 获取access_token，每次调用接口都调用此函数，不要记录
       /// </summary>
       /// <returns></returns>
        public static String GetAccessToken()
        {
            if (access_token == String.Empty ||
                DateTimeToTimeStamp(DateTime.Now) > timeStamp)
            {
                return RefreshAccessToken();
            }
            return access_token;
        }
    }
}
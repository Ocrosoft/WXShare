using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Xml.Serialization;

namespace WXShare
{
    /// <summary>
    /// 微信接口管理
    /// </summary>
    public class WXManage
    {
        // 公众号微信号
        private static String gh = "gh_7b4c81da71a3";
        // 公众号appID
        public static String appID = "wxe47cb091fd410b46";
        // 公众号appsecret
        private static String appsecret = "0a27cc9b2348b761de483e14dccd7cef";
        // access_token
        private static String access_token = String.Empty;
        // jsapi_ticket，一般与access_token同时获取和刷新
        private static String jsapi_ticket = String.Empty;
        // access_token下次要刷新的时间
        private static Int64 timeStamp;
        // 公众号设置的token
        public static String token = "chenyanhong";
        /// <summary>
        /// DateTime转UNIX时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static Int64 DateTimeToTimeStamp(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone
                .ToLocalTime(new DateTime(1970, 1, 1));
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
                    "https://api.weixin.qq.com/cgi-bin/token?grant_type=" +
                    "client_credential&appid=" +
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
                RefreshJsapiTicket();
                return access_token;
            }
            catch (WebException ex)
            {
                return "";
            }
        }
        /// <summary>
        /// 只能由RefreshAccessToken()调用
        /// </summary>
        /// <returns></returns>
        private static string RefreshJsapiTicket()
        {
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            try
            {
                // http请求
                request = WebRequest.Create(
                    "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" +
                    GetAccessToken() +
                    "&type=jsapi") as HttpWebRequest;
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
                // 获取ticket
                JObject jsonObject = (JObject)JsonConvert.DeserializeObject(content);
                jsapi_ticket = jsonObject["ticket"].ToString();
                return jsapi_ticket;
            }
            catch (WebException ex)
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
        /// <summary>
        /// 后去jsapi_ticket，每次调用接口都调用此函数，不要记录
        /// </summary>
        /// <returns></returns>
        public static string GetJsapiTicket()
        {
            if (access_token == String.Empty ||
                DateTimeToTimeStamp(DateTime.Now) > timeStamp)
            {
                RefreshAccessToken();
            }
            return jsapi_ticket;
        }
        /// <summary>
        /// 发起Post请求，url须自带access_token
        /// </summary>
        /// <param name="url">请求网址</param>
        /// <param name="postData">post数据</param>
        /// <param name="header">请求header信息</param>
        /// <returns></returns>
        public static JObject PostRequest(String url, String postData,
            WebHeaderCollection header = null)
        {
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            try
            {
                // http请求
                request = WebRequest.Create(url) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                if (header != null) request.Headers = header;
                var data = Encoding.UTF8.GetBytes(postData);
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
                return jsonObject;
            }
            catch (WebException ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 创建微信公众号的菜单
        /// </summary>
        /// <returns></returns>
        public static bool CreateMenu()
        {
            var menuJson = "{\"button\":[{\"type\":\"view\",\"name\":\"我的\"," +
                "\"url\":\"http://debug.ocrosoft.com/UserLogin.aspx\"}," +
                "{\"type\":\"view\",\"name\":\"当前活动\",\"url\":\"http://debug.ocrosoft.com/UserLogin.aspx\"}]}";
            var jsonObject = PostRequest(
                "https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" +
                GetAccessToken(), menuJson);
            if (jsonObject["errcode"].ToString() == "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RSAEnc(string content)
        {
            String publickey = @"<RSAKeyValue><Modulus>MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCbIGP2E/PxI0EEdIPMQTArwH+1
505lzC20EBCSy8aqMcJVZx5AbvYj/nbI4v+N7xlmS4kciOqj1zaGj9QoivKVjsBL
W6Hmhe8QCCw+MmR6jmWwLgmNAakOoDmlFjO8HVRZzTZEjiZX5LX9NG9FgYTGmZlJ
bnv48irG4BXyMpOdzQIDAQAB</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(publickey);
            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);

            return Convert.ToBase64String(cipherbytes);
        }
        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RSADec(string privatekey, string content)
        {
            privatekey = @"<RSAKeyValue><Modulus>MIICdwIBADANBgkqhkiG9w0BAQEFAASCAmEwggJdAgEAAoGBAJsgY/YT8/EjQQR0
g8xBMCvAf7XnTmXMLbQQEJLLxqoxwlVnHkBu9iP+dsji/43vGWZLiRyI6qPXNoaP
1CiK8pWOwEtboeaF7xAILD4yZHqOZbAuCY0BqQ6gOaUWM7wdVFnNNkSOJlfktf00
b0WBhMaZmUlue/jyKsbgFfIyk53NAgMBAAECgYB8oeNuG83MGVTtbWdOvbkkDb8N
uM9F/mth1d5a8pmkt+G4l+a4Qe5EMPfiom5L7KPtihaY9HAAPrKyHfCIukn3Gprv
4ysrFKm2wdiBh6VojJ+0L3X2IwEVQ/BZLuT37Qis7P7WXVE1tSV/J6nLW9fUZg+Z
XUmlzFAx2EiOWlK6AQJBAM4GFUGOFpyRvED0mNUdP3Jbx3fcf1v3/why4Deqx003
YiUMjXQpjLseb1vvvnSfZa34gqnFPKG/fFsU7owoLI0CQQDAwaDUlegMCTWAD89y
z+Ea98UlVCUqH+ldCVfC9RdhmT0OwrGvQDHEUmgBKKFTdM3n7FphycAMXKLvrrVb
6QZBAkEAjC/TcuHuPOdlg4VsIUdfjr8owUSGXNwo62TPcNGB/+a5n6Ak+G/1VLXm
7FX78Hstwu0ga8jL8vvK8GcT0sbbWQJANKTtcwIaJSdiuD4ZL0c9OKtQ6bgIim+6
wZEqqfFcWGiMt3pPIwkKTo8fHqnlHbD6B4ySxsBeNkIashFqMNb8wQJBAKIQqEyT
f+dynbjKdCccE7bALo1plmNgU82bU7vMOzHYHDsTAZ2CDmgAz78BZZbPiXDPOTOh
UXESmK2lfiIIg4M=</D></RSAKeyValue>";
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(privatekey);
            cipherbytes = rsa.Decrypt(Convert.FromBase64String(content), false);

            return Encoding.UTF8.GetString(cipherbytes);
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string MD5(string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(Encoding.Default.GetBytes(strText));
            return Encoding.Default.GetString(result);
        }
        /// <summary>
        /// 获取Aes32位密钥
        /// </summary>
        /// <param name="key">Aes密钥字符串</param>
        /// <returns>Aes32位密钥</returns>
        private static byte[] GetAesKey()
        {
            // aes密钥
            var key = "chenyanhong";
            if (key.Length < 32)
            {
                // 不足32补全
                key = key.PadRight(32, '0');
            }
            if (key.Length > 32)
            {
                key = key.Substring(0, 32);
            }
            return Encoding.UTF8.GetBytes(key);
        }
        /// <summary>
        /// Aes加密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="key">aes密钥，长度必须32位</param>
        /// <returns>加密后的字符串</returns>
        public static string EncryptAes(string source)
        {
            using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
            {
                aesProvider.Key = GetAesKey();
                aesProvider.Mode = CipherMode.ECB;
                aesProvider.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform cryptoTransform = aesProvider.CreateEncryptor())
                {
                    byte[] inputBuffers = Encoding.UTF8.GetBytes(source);
                    byte[] results = cryptoTransform.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                    aesProvider.Clear();
                    aesProvider.Dispose();
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }
        /// <summary>
        /// Aes解密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="key">aes密钥，长度必须32位</param>
        /// <returns>解密后的字符串</returns>
        public static string DecryptAes(string source)
        {
            using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
            {
                aesProvider.Key = GetAesKey();
                aesProvider.Mode = CipherMode.ECB;
                aesProvider.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor())
                {
                    byte[] inputBuffers = Convert.FromBase64String(source);
                    byte[] results = cryptoTransform.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                    aesProvider.Clear();
                    return Encoding.UTF8.GetString(results);
                }
            }
        }
        /// <summary>
        /// 获取二维码地址
        /// </summary>
        /// <param name="content">编码二维码的内容，网址必须http(s)开头</param>
        /// <returns></returns>
        public static string QRCode(string content)
        {
            /*
            bg	    背景颜色	bg=颜色代码，例如：bg=ffffff
            fg	        前景颜色	fg=颜色代码，例如：fg=cc0000
            gc	    渐变颜色	gc=颜色代码，例如：gc=cc00000
            el      	纠错等级	el可用值：h\q\m\l，例如：el=h
            w	        尺寸大小	w=数值（像素），例如：w=300
            m	        静区（外边距）	m=数值（像素），例如：m=30
            pt	        定位点颜色（外框）	pt=颜色代码，例如：pt=00ff00
            inpt	    定位点颜色（内点）	inpt=颜色代码，例如：inpt=000000
            logo	    logo图片	logo=图片地址，例如：logo=http://www.liantu.com/images/2013/sample.jpg
            */
            String api = "http://qr.liantu.com/api.php?text=";
            api += HttpUtility.UrlEncode(content);
            return api;
        }
        /// <summary>
        /// 添加客服
        /// </summary>
        /// <param name="account">客服账号名</param>
        /// <param name="nick">客服昵称</param>
        /// <returns></returns>
        public static bool AddKF(string account, string nick, string pass)
        {
            string postData = "{\"kf_account\":\"" + account + "@" + gh + "\",\"nickname\":\"" + nick + "\",\"password\":\"" + pass + "\"}";
            var json = PostRequest("https://api.weixin.qq.com/customservice/kfaccount/add?access_token=" + GetAccessToken(), postData);
            if (json["errcode"].ToString() == "0")
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// XML反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T FromXML<T>(string xml)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            StringReader sr = new StringReader(xml);
            T obj = (T)xs.Deserialize(sr);
            sr.Close();
            sr.Dispose();
            return obj;
        }
        /// <summary>
        /// XML序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static String ToXML<T>(T t)
        {
            XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
            xsn.Add(string.Empty, string.Empty);
            XmlSerializer xs = new XmlSerializer(typeof(T));
            StringWriter sw = new StringWriter();
            xs.Serialize(sw, t, xsn);
            string str = sw.ToString();
            sw.Close();
            sw.Dispose();
            return str;
        }
        /// <summary>
        /// 发送客服消息
        /// </summary>
        /// <param name="OPENID"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool SendMessage(string OPENID, string content)
        {
            var jsonStr = "{\"touser\":\"" + OPENID + "\",\"msgtype\":\"text\",\"text\":{\"content\":\"" + content + "\"}}";
            var json = PostRequest("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" +
                GetAccessToken(), jsonStr);
            if (json["errcode"].ToString() == "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断是否微信内置浏览器
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static bool IsWXBrowser(HttpRequest Request)
        {
            if (Request.UserAgent.IndexOf("MicroMessenger") == -1)
            {
                return false;
            }

            return true;
        }
        public static string WXJSSign(string nonce, Int64 timestamp, string url)
        {
            string str = "jsapi_ticket=" + GetJsapiTicket() +
                "&noncestr=" + nonce +
                "&timestamp=" + timestamp.ToString() +
                "&url=" + url;
            str = AuthCode.SHA1(str);
            return str;
        }
    }

    /// <summary>
    /// 消息基类(普通文本消息)
    /// </summary>
    [XmlRoot(ElementName = "xml")]
    public class XMLObject
    {
        public XMLObject()
        {
            CreateTime = WXManage.DateTimeToTimeStamp(DateTime.Now);
        }
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 发送方帐号（一个OpenID）
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        public Int64 CreateTime { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public string MsgType { get; set; }
        /// <summary>
        /// 转换为XML，子类必须重写(抄一遍实现，不然调用此方法报错)
        /// </summary>
        /// <returns></returns>
        public virtual  string ToXML()
        {
            CreateTime = WXManage.DateTimeToTimeStamp(DateTime.Now);
            return WXManage.ToXML(this);
        }
    }
    /// <summary>
    /// 文本消息，MsgType=text
    /// </summary>
    [XmlRoot(ElementName = "xml")]
    public class TextMessageXMLObject : XMLObject
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 消息id
        /// </summary>
        public string MsgId { get; set; }
        public override string ToXML()
        {
            CreateTime = WXManage.DateTimeToTimeStamp(DateTime.Now);
            return WXManage.ToXML(this);
        }
    }
    /// <summary>
    /// 订阅事件，MsgType=event
    /// </summary>
    [XmlRoot(ElementName = "xml")]
    public class SubscribeXMLObject : XMLObject
    {
        /// <summary>
        /// 事件类型，subscribe、unsubscribe
        /// </summary>
        public string Event { get; set; }
        public override string ToXML()
        {
            CreateTime = WXManage.DateTimeToTimeStamp(DateTime.Now);
            return WXManage.ToXML(this);
        }
    }
}
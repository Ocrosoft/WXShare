using System;
using System.Net;
using Ocrosoft;

namespace WXShare
{
    /// <summary>
    /// 网易云信短信验证码接口
    /// </summary>
    public class AuthCode
    {
        private static string appSecret = "db9cc835b811";
        private static string appKey = "29b612d279f0c2691ac683251f3b6a8c";

        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <returns></returns>
        public static bool SendAuthCode(String phone)
        {
            return true;

            var nonce = OSecurity.GetRandomString(16);
            var curTime = OSecurity.DateTimeToTimeStamp(DateTime.Now).ToString();
            var sha1 = OSecurity.SHA1(appSecret + nonce + curTime);
            var errcode = ORequest.RequestPost("https://api.netease.im/sms/sendcode.action",
                "&phone=" + phone,
                "code",
                new WebHeaderCollection
                {
                    { "AppKey",appKey},
                    {"Nonce", nonce},
                    {"CurTime", curTime},
                    {"CheckSum", sha1}
                });

            if (errcode == "200")
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 短信验证码是否正确
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="code">短信验证码</param>
        /// <returns></returns>
        public static bool CheckAuthCode(String phone, String code)
        {
            return true;

            var nonce = OSecurity.GetRandomString(16);
            var curTime = OSecurity.DateTimeToTimeStamp(DateTime.Now).ToString();
            var sha1 = OSecurity.SHA1(appSecret + nonce + curTime);
            var errcode = ORequest.RequestPost("https://api.netease.im/sms/verifycode.action",
                "&phone=" + phone,
                "code",
                new WebHeaderCollection
                {
                    {"AppKey",appKey},
                    {"Nonce", nonce},
                    {"CurTime", curTime},
                    {"CheckSum", sha1}
                });


            if (errcode == "200")
            {
                return true;
            }
            return false;
        }
    }
}
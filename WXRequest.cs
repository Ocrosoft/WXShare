using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Xml.Serialization;

namespace WXShare
{
    public class WXRequest : IHttpHandler
    {
        public bool IsReusable { get { return true; } }

        public void ProcessRequest(HttpContext context)
        {
            String postString = String.Empty;
            if (HttpContext.Current.Request.HttpMethod.ToUpper() == "POST")
            {
                using (Stream stream = HttpContext.Current.Request.InputStream)
                {
                    Byte[] postBytes = new Byte[stream.Length];
                    stream.Read(postBytes, 0, (Int32)stream.Length);
                    postString = Encoding.UTF8.GetString(postBytes);
                }

                if (!string.IsNullOrEmpty(postString))
                {
                    
                    Execute(postString);
                }
            }
            else
            {
                string token = WXManage.token;

                string echoString = HttpContext.Current.Request.QueryString["echoStr"];
                string signature = HttpContext.Current.Request.QueryString["signature"];
                string timestamp = HttpContext.Current.Request.QueryString["timestamp"];
                string nonce = HttpContext.Current.Request.QueryString["nonce"];

                if (CheckSignature(token, signature, timestamp, nonce))
                {
                    if (!string.IsNullOrEmpty(echoString))
                    {
                        HttpContext.Current.Response.Write(echoString);
                        HttpContext.Current.Response.End();
                    }
                }
            }
        }
        public bool CheckSignature(string token, string signature, string timestamp, string nonce)
        {
            string[] ArrTmp = { token, timestamp, nonce };

            Array.Sort(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);

            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();

            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Execute(string postString)
        {
            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
            // 获取ToUserName,FromUserName,CreateTime,MsgType
            var rec = WXManage.FromXML<XMLObject>(postString);
            if(rec.MsgType=="event")
            {
                // 事件类型处理
                // 仅处理关注
                var subEV = WXManage.FromXML<SubscribeXMLObject>(postString);
                if(subEV.Event == "subscribe")
                {
                    var res = new TextMessageXMLObject()
                    {
                        FromUserName = rec.ToUserName,
                        ToUserName = rec.FromUserName,
                        MsgType = "text",
                        Content = "欢迎关注\n回复数字balabala"
                    };
                    HttpContext.Current.Response.Write(res.ToXML());
                }
            }
            else if(rec.MsgType == "text")
            {
                // 文本类型处理
                var textMSG = WXManage.FromXML<TextMessageXMLObject>(postString);
                if(textMSG.Content == "1")
                {
                    TextMessageXMLObject res = new TextMessageXMLObject()
                    {
                        FromUserName = rec.ToUserName,
                        ToUserName = rec.FromUserName,
                        MsgType = "text",
                        Content = "你好"
                    };
                    HttpContext.Current.Response.Write(res.ToXML());
                }
            }
        }
    }
}
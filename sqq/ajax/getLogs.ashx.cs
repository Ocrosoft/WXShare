using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WXShare.sqq.ajax
{
    /// <summary>
    /// getLogs 的摘要说明
    /// </summary>
    public class getLogs : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            var logs = Sys.GetLog();
            context.Response.Write(JsonConvert.SerializeObject(logs));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
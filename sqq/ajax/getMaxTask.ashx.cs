using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WXShare.sqq.ajax
{
    /// <summary>
    /// 获取最大任务数
    /// </summary>
    public class getMaxTask : IHttpHandler
    {
        public class Ret
        {
            public int maxTask;
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            context.Response.Write(JsonConvert.SerializeObject(new Ret() { maxTask = Sys.MaxTask }));
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WXShare.sqq.ajax
{
    /// <summary>
    /// switchDispatch 的摘要说明
    /// </summary>
    public class switchDispatch : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            Sys.DispatchEnabled = !Sys.DispatchEnabled;
            Sys.Dispatching = false;
            Sys.Log((Sys.DispatchEnabled ? "开启" : "关闭") + "了任务分配");
            context.Response.Write(JsonConvert.SerializeObject(new getDispatch.Status() { status = Sys.DispatchEnabled }));
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
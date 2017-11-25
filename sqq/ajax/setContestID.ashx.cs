using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WXShare.sqq.ajax
{
    /// <summary>
    /// setContestID 的摘要说明
    /// </summary>
    public class setContestID : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var cid = context.Request.QueryString["cid"];
            if(cid == null)
            {
                return;
            }
            Sys.ContestID = int.Parse(cid);
            Sys.Log("设置比赛ID：" + cid);
            context.Response.Write(JsonConvert.SerializeObject(new getContestID.Ret() { contest_id = Sys.ContestID }));
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
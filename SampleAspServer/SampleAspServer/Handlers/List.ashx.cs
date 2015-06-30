using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleAspServer.Handlers
{
    /// <summary>
    /// Summary description for List
    /// </summary>
    public class List : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            //Accept Only GET requests
            if (context.Request.HttpMethod.ToUpper().Equals("GET"))
            {
                var requesterUserName = context.Request.Params.Get("RequesterUserName");
                if (String.IsNullOrEmpty(requesterUserName))
                {
                    context.Response.Write(JsonConvert.SerializeObject(new { error = true, message = "Please send a parameter with key value 'SenderUserName'" }));
                    return;
                }
                SampleAspServer.Models.SampleChatDBEntities db = new Models.SampleChatDBEntities();
                context.Response.Write(JsonConvert.SerializeObject(db.Messages.Where( x => x.receiverUserName.Equals(requesterUserName)).ToArray()));
            }
            else
            {
                context.Response.Write(JsonConvert.SerializeObject(new { error = true, message = "Please send a GET request instead of " + context.Request.HttpMethod }));
            }
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
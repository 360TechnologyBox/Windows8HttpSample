using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleAspServer.Handlers
{
    /// <summary>
    /// Summary description for ImageUpload
    /// </summary>
    public class ImageUpload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var file = context.Request.Files[0];
            if (file != null)
            {
                var path = context.Server.MapPath("~/Uploads/" + Guid.NewGuid() + "." + file.ContentType.Split('/')[1]);
                System.Diagnostics.Debug.Write(path);
                file.SaveAs(path);
                context.Response.Write(JsonConvert.SerializeObject(new { error = "false", message = "successfully uploaded." }));
            }
            else
            {
                context.Response.Write(JsonConvert.SerializeObject(new { error = "true", message = "please send an image file." }));
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
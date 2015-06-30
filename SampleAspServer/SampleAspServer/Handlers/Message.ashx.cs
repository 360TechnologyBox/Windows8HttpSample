using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleAspServer.Handlers
{
    /// <summary>
    /// Summary description for Message
    /// </summary>
    public class Message : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            //Accept Only POST requests
            //validate Parameters 
            if (context.Request.HttpMethod.ToUpper().Equals("POST"))
            {
                var parameters = context.Request.Params;
                var sender = parameters.Get("SenderUserName");
                var senderPhoneNumber  = parameters.Get("SenderPhoneNumber");
                var receiver = parameters.Get("ReceiverUserName");
                var receiverPhoneNumber = parameters.Get("ReceiverPhoneNumber");
                var messageData = parameters.Get("MessageData");

                //check for null values 
                if (String.IsNullOrEmpty(sender))
                {
                    context.Response.Write(JsonConvert.SerializeObject(new { error = true, message = "Please send a parameter with key value 'SenderUserName'" }));
                    return;
                }
                if (String.IsNullOrEmpty(receiver))
                {
                    context.Response.Write(JsonConvert.SerializeObject(new { error = true, message = "Please send a parameter with key value 'ReceiverUserName'" }));
                    return;
                }
                if (String.IsNullOrEmpty(messageData))
                {
                    context.Response.Write(JsonConvert.SerializeObject(new { error = true, message = "Please send a parameter with key value 'MessageData'" }));
                    return;
                }

                SampleAspServer.Models.Message m = new Models.Message();
                m.senderUserName = sender;
                m.senderPhoneNumber = senderPhoneNumber;
                m.receiverUserName = receiver;
                m.receiverPhoneNumber = receiverPhoneNumber;
                m.messageData = messageData;

                SampleAspServer.Models.SampleChatDBEntities db = new Models.SampleChatDBEntities();
                db.Messages.Add(m);
                db.SaveChanges();
                context.Response.Write(JsonConvert.SerializeObject(new { error = false , message = "Message Sent !" }));
                return;
            }
            else
            {
                context.Response.Write(JsonConvert.SerializeObject(new { error = true, message = "Please send a POST request instead of " + context.Request.HttpMethod }));
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
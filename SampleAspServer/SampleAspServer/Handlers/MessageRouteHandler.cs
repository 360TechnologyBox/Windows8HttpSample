using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace SampleAspServer.Handlers
{
    public class MessageRouteHandler : IRouteHandler
    {
        
        public IHttpHandler GetHttpHandler(System.Web.Routing.RequestContext requestContext)
        {
            var httpHandler = new Message();
            return httpHandler;
        }
    }
}
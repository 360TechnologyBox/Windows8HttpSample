using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace SampleAspServer
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);

            routes.Add(new Route("message/send", new SampleAspServer.Handlers.MessageRouteHandler()));
            routes.Add(new Route("message/list", new SampleAspServer.Handlers.ListRouteHandler()));
            routes.Add(new Route("image/upload", new SampleAspServer.Handlers.ImageUploadRouteHandler()));
        }
    }
}

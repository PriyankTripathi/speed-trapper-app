using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
namespace speed_trapper_app.SignalR
{
    public static class StartUp
    {
        public static void ConfigureSignalR(IAppBuilder app)
        {
            // For more information on how to configure your application using OWIN startup, visit http://go.microsoft.com/fwlink/?LinkID=316888

            app.MapSignalR();
        }
    }
}
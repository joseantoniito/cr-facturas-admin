using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartup(typeof(WebApp.App_Start.Startup1))]

namespace WebApp.App_Start
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            // Para obtener más información acerca de cómo configurar su aplicación, visite http://go.microsoft.com/fwlink/?LinkID=316888

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                //AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                AuthenticationType = "CookieAuthentication",
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
				LoginPath = new PathString("/Home/LogIn"),
				LogoutPath = new PathString("/Home/LogIn")
            });
        }
    }
}

using System;
using System.Threading.Tasks;
//using Microsoft.AspNet.Identity;
using Microsoft.Owin;
//using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(AspNetIdentity.Startup))]

namespace AspNetIdentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            //il runtime di owin legge questi parametri...
            ConfigureAuth(app);
            // Per altre informazioni su come configurare l'applicazione, vedere https://go.microsoft.com/fwlink/?LinkID=316888

         
        }
    }
}

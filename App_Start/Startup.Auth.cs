using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using AspNetIdentity.Models;
using Microsoft.AspNet.Identity;

namespace AspNetIdentity
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            //qui createperowincontext chiamerà una callback che istaziera un oggetto dbcontest ogni volta che ce ne sia bisogno
            //crea una singola istanza dell'oggetto per ogni richiesta dell'applicazione
            //l'oggetto è getito automaticamente dal lifecicle di aspnet
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            //anche qui callback per istanziare all'occorrenza classi ApplicationUserManager e userstore 
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            //anche qui callback chiamata per istanziare gli oggetti per l'autenticazione
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });

        }
    }
}
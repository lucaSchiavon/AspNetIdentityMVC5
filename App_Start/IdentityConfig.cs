using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using AspNetIdentity.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;

namespace AspNetIdentity
{
    //NB: implementando con una classe  IUserStore interface che viene implementata dalla classe buit in UserStore
    //è possibile definire logiche custom di memorizzazione dei dati senza utilizzare entity framework
    public class ApplicationUserStore : UserStore<ApplicationUser>
    {
        //In the constructor of the UserStore, requires us to pass the DBContext, so that it knows how to persist the data
        public ApplicationUserStore(ApplicationDbContext context)
            :base(context)
        {
        }
    }

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            :base(store)
        {
        }
        //per registrare nel contesto di owin a livello di middelware globale la classe ApplicationUserManager affinchè sia istanziata
        //all'occorrenza mediante la chiamata di una callback ad opera del runtime (la funzione che verra chiamata viene passata
        //al metodo app.CreatePerOwinContext
        //la funzione chiamata è il metodo statico seguente:
        //che di fatto espone una istanza di ApplicationUserManager
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            //context.Get ritorna una istanza di Applicationdbcontext registrata nel contesto di owin
            var store = new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>());
           
            var manager = new ApplicationUserManager(store);

            //regole di validazione generale
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
               AllowOnlyAlphanumericUserNames=false,
                RequireUniqueEmail=true
            };
            //regole validazione passowr
            manager.PasswordValidator = new PasswordValidator()
            {
                RequiredLength=6,
                RequireNonLetterOrDigit=false,
                RequireDigit=true,
                RequireLowercase=true,
                RequireUppercase=true
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
            

            return manager;
        }
    }

    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
           : base(userManager, authenticationManager)
        {
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
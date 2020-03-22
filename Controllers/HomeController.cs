using AspNetIdentity.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace AspNetIdentity.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
      [Authorize]
        public ActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                ViewBag.messaggio = "Utente autenticato " + User.Identity.Name;
               
            }
          
            return View();
           
            //else
            //{
            //    "utente non autenticato"
            //}


        }
        public async Task<string> AddUser()
        {
            ApplicationUser user;
            //per ora isanziamo qua ApplicationUserStore e ApplicationUserManager ed 
            //abbiamo creato una istanza di dbcontext
            //questo determina l'istanza di più istanze per richiesta
            //la soluzione consiste nel creare una sola istanza di userManager e dbcontext per richiesta.
            //ed utilizzarla per tutta la applicazione con il metodo CreatePerOwinContext dell'appbuilder
            ApplicationUserStore Store = new ApplicationUserStore(new ApplicationDbContext());
            ApplicationUserManager userManager = new ApplicationUserManager(Store);
           
            user = new ApplicationUser
            {
                UserName = "Testuser",
                Email = "TestUser@test.com"
            };
           
            var result = await userManager.CreateAsync(user);
            //il valore di ritorno è un identityreult con una proprietà succeeded
            if (!result.Succeeded)
            {
                return result.Errors.First();
            }
            return "User added";
        }
    }
}
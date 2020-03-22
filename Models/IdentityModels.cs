using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin;

namespace AspNetIdentity.Models
{
    public class ApplicationUser :IdentityUser
    {
        //You can extend this class by adding additional fields like Birthday
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            //qui faccio il passaggio di argomenti al costruttore della classe base...
            : base ("DefaultConnection",throwIfV1Schema:false)
        { }
        //per utilizzare il metodo CreatePerOwinContext e registrare il middleware dobbiamo creare un metodo statico
        //che esponga una istanza di dbcontext
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
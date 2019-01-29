using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Data.Entity.Validation;
using TIT.Management.Models;

[assembly: OwinStartupAttribute(typeof(TIT.Management.Startup))]
namespace TIT.Management
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User     
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool    
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                   

                var user = new ApplicationUser();
                user.UserName = "admin";
                user.FullName = "admin";
                user.Email = "admin@gmail.com";

                string userPWD = "Admin@123456789";

                try
                {
                    var chkUser = UserManager.Create(user, userPWD);
                    //Add default User to Role Admin    
                    if (chkUser.Succeeded)
                    {
                        var result1 = UserManager.AddToRole(user.Id, "Admin");

                    }
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        //Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        //    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            //Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            //    ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                }
            }
            // creating Creating Manager role     
            if (!roleManager.RoleExists("BranchManager"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "BranchManager";
                roleManager.Create(role);

            }


            // creating Creating Employee role     
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);

            }

            // creating Creating Employee role     
            if (!roleManager.RoleExists("Accountant"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Accountant";
                roleManager.Create(role);

            }
        }
    }
}

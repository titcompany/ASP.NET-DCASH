using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TIT.Management.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public int ManageLevel { get; set; }


        public string ManagerId { get; set; }
        public int CuaHang_Id { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("IdentityDbContext", throwIfV1Schema: false)
        {
            /*System.Data.Entity.Database.SetInitializer(
                new System.Data.Entity.MigrateDatabaseToLatestVersion<TITTraffic.Models.ApplicationDbContext, Db>());*/
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
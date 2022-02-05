using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DMS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
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
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<DMS.Models.OrganizationAddEditModel> OrganizationAtEditModels { get; set; }

        public System.Data.Entity.DbSet<DMS.Models.PersonAddEditModel> PersonAddEditModels { get; set; }

        public System.Data.Entity.DbSet<DMS.Models.OfferAddEditModel> OfferAddEditModels { get; set; }

        public System.Data.Entity.DbSet<DMS.Models.AgreementAddEditModel> AgreementAddEditModels { get; set; }

        public System.Data.Entity.DbSet<DMS.Models.InvoiceAddEditModel> InvoiceAddEditModels { get; set; }

        public System.Data.Entity.DbSet<DMS.Models.NoticeAddEditModel> NoticeAddEditModels { get; set; }
    }
}
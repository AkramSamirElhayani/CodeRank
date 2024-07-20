 
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeRank.API.Identity
{
    public class ApplicationIdentityDbContext :IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
           : base(options)
        {
            Database.Migrate();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Api_Cats.Entities
{
    public class CatsDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public CatsDbContext(DbContextOptions<CatsDbContext> options) : base(options)
        {
            
        }
        
        public DbSet<Cat> Cats { get; set; }
        public DbSet<Product> Products { get; set; }       

    }
}

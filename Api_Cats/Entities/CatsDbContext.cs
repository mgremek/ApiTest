using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using System.Security.Cryptography.X509Certificates;

namespace Api_Cats.Entities
{
    public class CatsDbContext : DbContext
    {
        public CatsDbContext(DbContextOptions<CatsDbContext> options) : base(options)
        {
            
        }
        
        public DbSet<Human> Human { get; set; }
        public DbSet<Cat> Cats { get; set; }

    }
}

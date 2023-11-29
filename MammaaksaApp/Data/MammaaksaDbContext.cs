using MammaaksaApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MammaaksaApp.Data
{
    public class MammaaksaDbContext: DbContext
    {
        public MammaaksaDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<Mamaaksa> Mammaaksa { get; set; }
        public DbSet<Account> accounts { get; set; }

    }
}

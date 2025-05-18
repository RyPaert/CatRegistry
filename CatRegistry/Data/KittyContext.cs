using CatRegistry.Domain;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace CatRegistry.Data
{
    public class KittyContext : DbContext
    {
        public KittyContext(DbContextOptions<KittyContext> options) : base(options) { }
            public DbSet<Kitty> Kittys { get; set; }
            public DbSet<FileToDatabase> FileToDatabase {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Kitty>().ToTable("Kittys")
        }
    }
}

using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Anima.Student.Infra.Data.Context.MySql
{
    public class MySqlContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseMySql(config.GetSection("ConnectionStrings:Connection").Value);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries()
                .Where(entry => entry.Entity.GetType().GetProperty("InsertAt") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("InsertAt").CurrentValue = DateTime.Now;

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("InsertAt").IsModified = false;
                }
            }

            foreach (var entry in ChangeTracker.Entries()
                .Where(entry => entry.Entity.GetType().GetProperty("UpdateAt") != null))
            {
                if (entry.State == EntityState.Modified)
                    entry.Property("UpdateAt").CurrentValue = DateTime.Now;
            }
            
            return base.SaveChangesAsync(cancellationToken);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<PartnerDto>().ToTable("partner", "ZeDb");
            //modelBuilder.Entity<AddressDto>().ToTable("address", "ZeDb");
            //modelBuilder.Entity<CoverageAreaDto>().ToTable("coveragearea", "ZeDb");
            //modelBuilder.Entity<CoverageAreaValuesDto>().ToTable("coverageareavalues", "ZeDb");
            
            base.OnModelCreating(modelBuilder);
        }

        //public DbSet<PartnerDto> Partner { get; set; }
        //public DbSet<AddressDto> Address { get; set; }
        //public DbSet<CoverageAreaDto> CoverageArea { get; set; }
        //public DbSet<CoverageAreaValuesDto> CoverageAreaValues { get; set; }
    }
}
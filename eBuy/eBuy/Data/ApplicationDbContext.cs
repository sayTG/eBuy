using eBuy.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eBuy.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Products>(builder =>
            {
                builder.Property(e => e.Id).ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            });
            SampleData.SeedUsers(builder);
            SampleData.SeedRoles(builder);
            SampleData.SeedUserRoles(builder);
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
    }
}

using eBuy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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
            SampleData.SeedUsers(builder);
            SampleData.SeedRoles(builder);
            SampleData.SeedUserRoles(builder);
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
    }
}

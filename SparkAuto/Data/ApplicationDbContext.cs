using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Models;
using SparkAuto.Pages.Models;

namespace SparkAuto.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ServiceType> serviceTypes { get; set; }

        public DbSet<ApplicationUser> applicationUsers { get; set; }

        public DbSet<Car> Car { get; set; }

        public DbSet<ServiceShoppingCart> serviceShoppingCarts { get; set; }

        public DbSet<ServiceHeader> serviceHeaders { get; set; }

        public DbSet<ServiceDetails> serviceDetails { get; set; }
    }
}

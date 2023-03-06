using System;
using System.Collections.Generic;
using System.Text;
using Backend.SalesManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.SalesManagement.Context
{
    public class SalesDatabaseContext : DbContext
    {
        public SalesDatabaseContext(
            DbContextOptions<SalesDatabaseContext> dbContextOptions)
            : base(dbContextOptions) { }

        public DbSet<Sales> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sales>()
                .HasKey(x => x.Id);
        }
    }
}

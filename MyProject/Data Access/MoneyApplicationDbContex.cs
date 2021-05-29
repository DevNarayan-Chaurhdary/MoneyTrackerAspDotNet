using Microsoft.EntityFrameworkCore;
using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Data_Access
{
    public class MoneyApplicationDbContex : DbContext
    {

            public MoneyApplicationDbContex(DbContextOptions dbContextOptions):base(dbContextOptions)
            {
                
            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                //base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<Category>()
                    .ToTable("MST_Category");

                modelBuilder.Entity<PaymentMode>()
                    .ToTable("MST_Payment_Mode");

                modelBuilder.Entity<Transaction>()
                    .ToTable("Transactions");
        

            }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                //base.OnConfiguring(optionsBuilder);
                //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-QFMI77O;Initial Catalog=MoneyTrackerContex;Integrated Security=True");
            }

            public DbSet<Category> Category { get; set; }

            public DbSet<PaymentMode> PaymentMode { get; set; }

            public DbSet<Transaction> Transactions { get; set; }
 
    }
}

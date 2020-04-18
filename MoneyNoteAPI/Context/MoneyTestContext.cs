using Microsoft.EntityFrameworkCore;
using MoneyNoteLibrary.Common;
using MoneyNoteLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyNoteAPI.Context
{
    public class MoneyTestContext : DbContext
    {
        public DbSet<MoneyItem> MoneyItems { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<MainCategory> MainCategories { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<BankBook> BankBooks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            var connectionString = AzureKeyVault.OnGetAsync(KeyVaultName.MoneyNoteTestConnection.ToString()).Result;
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
             .HasMany(x => x.MoneyItems)
             .WithOne(y => y.User)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(x => x.MainCategories)
                .WithOne(y => y.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
              .HasMany(x => x.BankBooks)
              .WithOne(y => y.User)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MoneyItem>()
                .HasOne(x => x.MainCategory)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<MoneyItem>()
            //    .HasOne(x => x.User)
            //    .WithMany()
            //    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MoneyItem>()
                .HasOne(x => x.BankBook)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<MainCategory>()
            //    .HasOne(x => x.User)
            //    .WithMany(y => y.MainCategories)
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<BankBook>()
            //    .HasMany(x => x.MoneyItems)
            //    .WithOne(y => y.BankBook)
            //    .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}

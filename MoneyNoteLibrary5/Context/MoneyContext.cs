using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using MoneyNoteLibrary.Common;
using MoneyNoteLibrary5.Models;
using MoneyNoteLibrary5.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyNoteLibrary5.Context
{
    public class MoneyContext : DbContext
    {
        public MoneyContext() { }

        public MoneyContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<MoneyItem> MoneyItems { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<MainCategory> MainCategories { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<BankBook> BankBooks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(AzureKeyVault.OnGetAsync(KeyVaultName.MoneyNoteConnectionString).Result);
                optionsBuilder.UseSqlServer(AzureKeyVault.OnGetAsync(KeyVaultName.MoneyNoteConnectionString).Result, x => x.MigrationsAssembly("MoneyNoteLibrary5"));
            }
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
                .WithMany(y => y.MoneyItems)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            //modelBuilder.Entity<MainCategory>()
            //    .HasOne(x => x.User)
            //    .WithMany(y => y.MainCategories)
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<BankBook>()
            //    .HasMany(x => x.MoneyItems)
            //    .WithOne(y => y.BankBook)
            //    .OnDelete(DeleteBehavior.SetNull);

            //base.OnModelCreating(modelBuilder);
        }
    }
}

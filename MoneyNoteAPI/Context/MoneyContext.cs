using Microsoft.EntityFrameworkCore;
using MoneyNoteLibrary.Common;
using MoneyNoteLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyNoteAPI.Context
{
    public class MoneyContext : DbContext
    {
        public DbSet<MoneyItem> MoneyItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            var connectionString = AzureKeyVault.OnGetAsync("MoneyNoteConnectionString").Result;
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}

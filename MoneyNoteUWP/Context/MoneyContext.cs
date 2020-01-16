using Microsoft.EntityFrameworkCore;
using MoneyNoteLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyNote.Context
{
    public class MoneyContext : DbContext
    {
        public DbSet<MoneyItem> MoneyItems { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=moneydb.db");
        }
    }
}

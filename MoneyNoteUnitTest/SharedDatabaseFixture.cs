using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MoneyNoteLibrary;
using MoneyNoteLibrary.Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace MoneyNoteUnitTest
{
    public class SharedDatabaseFixture : IDisposable
    {
        private string ConnectionSting { get; set; }

        public SharedDatabaseFixture()
        {
            ConnectionSting = AzureKeyVault.OnGetAsync(KeyVaultName.MoneyNoteTestConnection.ToString()).Result;
            Connection = new SqlConnection(ConnectionSting);
            Connection.Open();
        }

        public DbConnection Connection { get; }

        public MoneyContext CreateContext(DbTransaction transaction = null)
        {
            var context = new MoneyContext(new DbContextOptionsBuilder<MoneyContext>().UseSqlServer(Connection).Options, ConnectionSting);

            if (transaction != null)
            {
                context.Database.UseTransaction(transaction);
            }

            return context;
        }

        public void Dispose() => Connection.Dispose();
    }
}

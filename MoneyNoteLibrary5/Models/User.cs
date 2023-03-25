using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MoneyNoteLibrary5.Models
{
    public class User : ICommon
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public List<MoneyItem> MoneyItems { get; set; }

        public List<MainCategory> MainCategories { get; set; }

        public List<BankBook> BankBooks { get; set; }

        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset UpdatedTime { get; set; }

        public bool IsApproved { get; set; } = false;
    }
}

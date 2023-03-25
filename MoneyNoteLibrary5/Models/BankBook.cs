using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MoneyNoteLibrary5.Models
{
    public class BankBook : ICommon
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double Assets { get; set; } = 0;

        public User User { get; set; }

        public Guid UserId { get; set; }

        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset UpdatedTime { get; set; }
    }
}

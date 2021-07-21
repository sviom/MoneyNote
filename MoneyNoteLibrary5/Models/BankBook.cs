using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MoneyNoteLibrary5.Models
{
    public class BankBook : ICommon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Assets { get; set; } = 0;

        [Required]
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset UpdatedTime { get; set; } = DateTimeOffset.Now;

        [Required]
        public User User { get; set; }

        public Guid UserId { get; set; }

        public List<MoneyItem>? MoneyItems { get; set; }

    }
}

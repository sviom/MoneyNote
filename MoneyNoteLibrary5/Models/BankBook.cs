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

        // Introducing FOREIGN KEY constraint 'FK_MoneyItems_BankBooks_BankBookId' on table 'MoneyItems' may cause cycles or multiple cascade paths.
        // Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints.
        // 위의 문제로 수동으로 null 처리
        // public List<MoneyItem> MoneyItems { get; set; } = new List<MoneyItem>();

    }
}

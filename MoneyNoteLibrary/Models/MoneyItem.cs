using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MoneyNoteLibrary.Models
{
    public class MoneyItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public double Money { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset UpdatedTime { get; set; } = DateTimeOffset.Now;
    }
}

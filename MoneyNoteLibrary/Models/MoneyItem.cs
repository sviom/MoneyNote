using MoneyNoteLibrary.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static MoneyNoteLibrary.Enums.MoneyEnum;

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
        public MoneyCategory Division { get; set; }

        [Required]
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset UpdatedTime { get; set; } = DateTimeOffset.Now;

        public Guid UserId { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public MainCategory MainCategory { get; set; }

        public Guid MainCategoryId { get; set; }

        public SubCategory SubCategory { get; set; }
    }
}

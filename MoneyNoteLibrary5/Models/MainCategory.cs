using MoneyNoteLibrary5.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static MoneyNoteLibrary5.Enums.MoneyEnum;

namespace MoneyNoteLibrary5.Models
{
    public class MainCategory : ICommon, ICategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public MoneyCategory Division { get; set; }

        [Required]
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset UpdatedTime { get; set; }

        public List<SubCategory> SubCategories { get; set; }
    }
}

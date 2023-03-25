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
        public Guid Id { get; set; }

        public User User { get; set; }

        public Guid UserId { get; set; }

        public string Title { get; set; }

        public MoneyCategory Division { get; set; }

        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset UpdatedTime { get; set; }

        public List<SubCategory> SubCategories { get; set; }
    }
}

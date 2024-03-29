﻿using MoneyNoteLibrary5.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static MoneyNoteLibrary5.Enums.MoneyEnum;

namespace MoneyNoteLibrary5.Models
{
    public class MoneyItem : ICommon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Title { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public double Money { get; set; }

        public string Description { get; set; }

        [Required]
        public MoneyCategory Division { get; set; }

        [Required]
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset UpdatedTime { get; set; }

        public MainCategory MainCategory { get; set; }

        public Guid MainCategoryId { get; set; }

        public SubCategory SubCategory { get; set; }

        public Guid SubCategoryId { get; set; }

        public BankBook BankBook { get; set; }

        public Guid BankBookId { get; set; }
    }
}

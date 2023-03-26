﻿using System;
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
        public User User { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset UpdatedTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyNoteLibrary.Models
{
    public class MoneyItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public double Money { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}

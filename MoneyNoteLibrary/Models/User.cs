using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyNoteLibrary.Models
{
    public class User : ICommon
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}

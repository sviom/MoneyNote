using System;
using System.Collections.Generic;
using System.Text;
using static MoneyNoteLibrary.Enums.MoneyEnum;

namespace MoneyNoteLibrary.Models
{
    public interface ICommon
    {
        Guid Id { get; set; }
    }

    public interface ICategory
    {
        string Title { get; set; }
        MoneyCategory Division { get; set; }
    }
}

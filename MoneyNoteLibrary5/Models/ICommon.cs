using System;
using System.Collections.Generic;
using System.Text;
using static MoneyNoteLibrary5.Enums.MoneyEnum;

namespace MoneyNoteLibrary5.Models
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

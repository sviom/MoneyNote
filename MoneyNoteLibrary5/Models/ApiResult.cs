using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyNoteLibrary5.Models
{
    public class ApiResult<T>
    {
        public ApiResult() { }

        public ApiResult(T item) : base() => Content = item;

        public T Content { get; set; }

        public bool Result { get; set; }

        public string ResultMessage { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyNoteLibrary.Models
{
    public class ApiRequest<T>
    {
        public ApiRequest() { }

        public ApiRequest(T item) : base() => Content = item;

        public T Content { get; set; }
    }
}

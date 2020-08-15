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

    public class ApiRequest<T, U>
    {
        public ApiRequest() { }

        public ApiRequest(T item, U subItem) : base()
        {
            Content = item;
            SubContent = subItem;
        }

        public T Content { get; set; }

        public U SubContent { get; set; }
    }
}

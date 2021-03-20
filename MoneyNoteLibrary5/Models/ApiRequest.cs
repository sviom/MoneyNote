using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyNoteLibrary5.Models
{
    public interface IApiRequest
    {
    }

    public class ApiRequest<T> : IApiRequest
    {
        public ApiRequest() { }

        public ApiRequest(T item) : base() => Content = item;

        public T Content { get; set; }
    }

    public class ApiRequest<T, U> : IApiRequest
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

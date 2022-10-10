using System;
using Microsoft.AspNetCore.Mvc;
using MoneyNoteLibrary5.Models;


namespace MoneyNoteAPI.Models
{
    public class ApiActionResult<T>: ControllerBase, IApiResult
    {
       
        public ApiActionResult(T item) : base() => Content = item;

        public new T Content { get; set; }

        public bool Result { get; set; }
        public string ResultMessage { get; set; }

        public ApiActionResult()
        {
        }

    }
}


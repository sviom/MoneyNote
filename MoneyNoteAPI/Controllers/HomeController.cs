﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoneyNoteAPI.Services;
using MoneyNoteLibrary5.Common;
using MoneyNoteLibrary5.Models;
using Newtonsoft.Json;

namespace MoneyNoteAPI.Views
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

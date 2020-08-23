﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyNoteLibrary.Common;
using MoneyNoteLibrary.Enums;
using MoneyNoteLibrary.Models;
using Newtonsoft.Json;

namespace MoneyNoteWebAdmin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            var request = new ApiRequest<bool>(true);

            var result = await MoneyApiInfo.MoneyApi.GetUsers.ApiLauncher<List<User>>(request, MoneyApiInfo.ControllerEnum.user);

            return result.Content.ToArray();
        }
    }
}
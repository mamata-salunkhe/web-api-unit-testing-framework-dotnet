using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DateTimeController : ControllerBase
    {
        [HttpGet(Name = "DateTime")]
        public string Get()
        {
            return DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
        }
    }
}

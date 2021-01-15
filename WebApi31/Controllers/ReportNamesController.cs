using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetStandard20ToWcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi31.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportNamesController : ControllerBase
    {
        [HttpGet]
        public async Task<string> GetAsync()
        {
            var catalogItems = await new ReportUtilities().GetReportsAsync();
            return string.Join("\n", catalogItems.Select(i => i.Name).ToList());
        }
    }
}

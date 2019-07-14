using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServicesCheckerLib.Interfaces;

namespace ServiceCheckerWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCheckerController : Controller
    {
        private readonly IHistoryLoader _historyLoader;

        public ServiceCheckerController(IHistoryLoader historyLoader)
        {
            _historyLoader = historyLoader;
        }

        [HttpGet]
        public async Task<ActionResult<List<IServiceStatusElement>>> Get()
        {            
            return await _historyLoader.Load(10);
        }
    }
}
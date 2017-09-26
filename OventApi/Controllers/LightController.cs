using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OventService;

namespace OventApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Light")]
    public class LightController : Controller
    {
        public OventBusinessService OventService { get; }

        public LightController(OventBusinessService oventService)
        {
            this.OventService = oventService;
        }

        [HttpGet("{room}/{state}")]
        public async Task<IActionResult> Get(string room, string state)
        {
            await OventService.HandleLightsAsync(state, room);
            return Ok();
        }

    }
}
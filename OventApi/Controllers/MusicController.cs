﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OventService;

namespace OventApi.Controllers
{
    [Produces("application/json")]
    [Route("api/music")]
    public class MusicController : Controller
    {
        public OventBusinessService OventService { get; }

        public MusicController(OventBusinessService oventService)
        {
            this.OventService = oventService;
        }

        [HttpGet("playlist/{number:int}")]
        public async Task<IActionResult> Playlist(int number)
        {
            await OventService.HandlePlaylistAsync(number);
            return Ok();
        }

        [HttpGet("power")]
        public async Task<IActionResult> Power()
        {
            await OventService.PowerMusiqueAsync();
            return Ok();
        }

    }
}
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
    [Route("api/tv")]
    public class TvController : Controller
    {
        public OventBusinessService OventService { get; }

        public TvController(OventBusinessService oventService)
        {
            this.OventService = oventService;
        }

        [HttpGet("power/{state}")]
        public async Task<IActionResult> Power(string state)
        {
            await OventService.AllumerLaTéléAsync(state == "on");
            return Ok();
        }

        [HttpGet("volume/{volume:int}")]
        public async Task<IActionResult> Power(int volume)
        {
            await OventService.SetKodiVolumn(volume);
            return Ok();
        }

        [HttpGet("seek/{seconds:int}")]
        public async Task<IActionResult> Seek(int seconds)
        {
            await OventService.SeekKodi(seconds);
            return Ok();
        }

        [HttpGet("mute")]
        public async Task<IActionResult> Mute()
        {
            await OventService.MuteKodi();
            return Ok();
        }

        [HttpGet("stop")]
        public async Task<IActionResult> Stop()
        {
            await OventService.StopKodi();
            return Ok();
        }


        [HttpGet("pause")]
        public async Task<IActionResult> Pause()
        {
            await OventService.PauseKodi();
            return Ok();
        }

        [HttpGet("play/next/{serie}")]
        public async Task<IActionResult> Next(string serie)
        {
            serie = serie.Replace("de ", String.Empty).Replace("d ' ", String.Empty);
            if (await OventService.PlayNextEpisodeAsync(serie))
            {
                return Ok();
            }
            else
            {
                return NotFound();

            }
        }

        [HttpGet("play/{movie}")]
        public async Task<IActionResult> Movie(string movie)
        {
            if (await OventService.PlayMovie(movie))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

    }
}
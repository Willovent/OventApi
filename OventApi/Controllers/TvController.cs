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

        [HttpGet("play/next/{serie}")]
        public async Task<IActionResult> Next(string serie)
        {
            serie = serie.Replace("de ", String.Empty);
            try
            {
                await OventService.PlayNextEpisodeAsync(serie);
                return Ok();

            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("play/{movie}")]
        public async Task<IActionResult> Movie(string movie)
        {
            movie = movie.Replace("de ", String.Empty);
            try
            {
                await OventService.PlayMovie(movie);
                return Ok();

            }
            catch
            {
                return NotFound();
            }
        }

    }
}
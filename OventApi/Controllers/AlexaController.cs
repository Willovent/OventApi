using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OventService;
using System.Threading.Tasks;

namespace OventApi.Controllers
{
    [Produces("application/json")]
    [Route("api/alexa")]
    public class AlexaController : Controller
    {
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> HandleAlexa([FromBody]SkillRequest request, [FromServices] KodiService kodiService, [FromServices]ILoggerFactory loggerFactory)
        {
            var requestType = request.GetRequestType();
            var logger = loggerFactory.CreateLogger("Alexa");
            if (requestType == typeof(LaunchRequest))
            {
                var okL = new Alexa.NET.Response.SsmlOutputSpeech();
                okL.Ssml = "<speak>Ok !</speak>";
                var responseL = ResponseBuilder.Tell(okL);
                responseL.Response.ShouldEndSession = false;
                return this.Ok(responseL);
            }
            else if (requestType == typeof(IntentRequest))
            {
                var intentRequest = request.Request as IntentRequest;
                logger.LogInformation($"Received Intent: {intentRequest.Intent.Name} whith Slots : {JsonConvert.SerializeObject(intentRequest.Intent.Slots, Formatting.Indented)}");
                switch (intentRequest.Intent.Name)
                {
                    case "PlayerSeekForward":
                        await kodiService.Seek(30);
                        break;
                    case "PlayerSeekBackward":
                        await kodiService.Seek(-30);
                        break;
                    case "PlayerSeekSmallForward":
                        await kodiService.ForwardSmall();
                        break;
                    case "PlayerSeekSmallBackward":
                        await kodiService.BackwardSmall();
                        break;
                    case "PlayerSeekBigForward":
                        await kodiService.ForwardBig();
                        break;
                    case "PlayerSeekBigBackward":
                        await kodiService.BackwardBig();
                        break;
                    case "WatchEpisode":
                        if (!await kodiService.PlayEpisodeAsync(intentRequest.Intent.Slots["Show"].Value, int.Parse(intentRequest.Intent.Slots["Season"].Value), int.Parse(intentRequest.Intent.Slots["Episode"].Value)))
                        {
                            var notFoundText = new Alexa.NET.Response.SsmlOutputSpeech();
                            notFoundText.Ssml = "<speak>Episode non trouvée</speak>";
                            var notFountResponse = ResponseBuilder.Tell(notFoundText);
                            notFountResponse.Response.ShouldEndSession = false;
                            return this.Ok(notFountResponse);
                        }
                        break;
                    case "WatchLatestEpisode":
                        if (!await kodiService.PlayLatestEpisodeAsync(intentRequest.Intent.Slots["Show"].Value))
                        {
                            var notFoundText = new Alexa.NET.Response.SsmlOutputSpeech();
                            notFoundText.Ssml = "<speak>Série non trouvée</speak>";
                            var notFountResponse = ResponseBuilder.Tell(notFoundText);
                            notFountResponse.Response.ShouldEndSession = false;
                            return this.Ok(notFountResponse);
                        }
                        break;
                    case "WatchNextEpisode":
                        if (!await kodiService.PlayNextEpisodeAsync(intentRequest.Intent.Slots["Show"].Value))
                        {
                            var notFoundText = new Alexa.NET.Response.SsmlOutputSpeech();
                            notFoundText.Ssml = "<speak>Série non trouvée</speak>";
                            var notFountResponse = ResponseBuilder.Tell(notFoundText);
                            notFountResponse.Response.ShouldEndSession = false;
                            return this.Ok(notFountResponse);
                        }
                        break;
                    case "WatchLastShow":
                        await kodiService.PlayLastShowAsync();
                        break;
                    case "WatchMovie":
                        if (!await kodiService.PlayMovieAsync(intentRequest.Intent.Slots["Movie"].Value))
                        {
                            var notFoundText = new Alexa.NET.Response.SsmlOutputSpeech();
                            notFoundText.Ssml = "<speak>Film non trouvé</speak>";
                            var notFountResponse = ResponseBuilder.Tell(notFoundText);
                            notFountResponse.Response.ShouldEndSession = false;
                            return this.Ok(notFountResponse);
                        }
                        break;
                    case "WatchRandomMovie":
                        await kodiService.PlayRandomMovieAsync();
                        break;
                    case "Home":
                        await kodiService.Home();
                        break;
                    case "Back":
                        await kodiService.Back();
                        break;
                    case "Info":
                        await kodiService.Info();
                        break;
                    case "Up":
                        await kodiService.Up();
                        break;
                    case "Down":
                        await kodiService.Down();
                        break;
                    case "Right":
                        await kodiService.Right();
                        break;
                    case "Left":
                        await kodiService.Left();
                        break;
                    case "Select":
                        await kodiService.Select();
                        break;
                    case "Menu":
                        await kodiService.Menu();
                        break;
                    case "PageUp":
                        await kodiService.PageUp();
                        break;
                    case "PageDown":
                        await kodiService.PageDown();
                        break;
                    case "ViewMovies":
                        await kodiService.ViewMovies();
                        break;
                    case "ViewShows":
                        await kodiService.ViewShows();
                        break;
                    case "ViewMoviePlaylistRecent":
                        await kodiService.ViewMoviePlaylistRecent();
                        break;
                    case "ViewEpisodePlaylistRecent":
                        await kodiService.ViewEpisodePlaylistRecent();
                        break;
                    case "Fullscreen":
                        await kodiService.FullScreen();
                        break;
                    case "Mute":
                        await kodiService.Mute();
                        break;
                    case "VolumeUp":
                        await kodiService.VolumeUp();
                        break;
                    case "VolumeDown":
                        await kodiService.VolumeDown();
                        break;
                    case "VolumeSet":
                        await kodiService.Volume(int.Parse(intentRequest.Intent.Slots["Volume"].Value));
                        break;
                    case "VolumeSetPct":
                        await kodiService.Volume(int.Parse(intentRequest.Intent.Slots["Volume"].Value));
                        break;
                    case "SubtitlesOn":
                        await kodiService.SubtitleOn();
                        break;
                    case "SubtitlesOff":
                        await kodiService.SubtitleOff();
                        break;
                    case "SubtitlesNext":
                        await kodiService.SubtitleNext();
                        break;
                    case "SubtitlesPrevious":
                        await kodiService.SubtitlePrevious();
                        break;
                    case "SubtitlesDownload":
                        await kodiService.SubtitleDownload();
                        break;
                    case "AudioStreamNext":
                        await kodiService.AudioStreamNext();
                        break;
                    case "AudioStreamPrevious":
                        await kodiService.AudioStreamPrevious();
                        break;
                    case "Hibernate":
                        await kodiService.Hibernate();
                        break;
                    case "Reboot":
                        await kodiService.Reboot();
                        break;
                    case "Shutdown":
                        await kodiService.Shutdown();
                        break;
                    case "Suspend":
                        await kodiService.Suspend();
                        break;
                    case "AMAZON.CancelIntent":
                        break;
                    case "AMAZON.StopIntent":
                        await kodiService.Stop();
                        break;
                    case "AMAZON.NoIntent":
                        break;
                    case "AMAZON.YesIntent":
                        break;
                    case "AMAZON.NextIntent":
                        break;
                    case "AMAZON.PauseIntent":
                        await kodiService.Pause();
                        break;
                    case "AMAZON.PreviousIntent":
                        break;
                    case "AMAZON.ResumeIntent":
                        await kodiService.Pause();
                        break;
                    case "AMAZON.StartOverIntent":
                        break;
                    case "AMAZON.ShuffleOnIntent":
                        break;
                    case "AMAZON.ShuffleOffIntent":
                        break;
                    case "AMAZON.LoopOnIntent":
                        break;
                    case "AMAZON.LoopOffIntent":
                        break;
                    case "AMAZON.HelpIntent":
                        break;
                }
            }
            else if (requestType == typeof(SessionEndedRequest))
            {
                return this.Ok(ResponseBuilder.Empty());
            }
            var response = ResponseBuilder.Empty();
            response.Response.ShouldEndSession = false;
            return this.Ok(response);
        }
    }
}
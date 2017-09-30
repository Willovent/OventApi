using System.Threading.Tasks;
using System.Collections.Generic;

namespace OventService
{
    /// <summary>
    /// Service permettant de gérer les équipements connectés chez Will
    /// </summary>
    public class OventBusinessService
    {

        public OventBusinessService(string zwayApiHost, string kodiHost, string kodiUserName, string kodiPassword)
        {
            this.apiService = new OventApiService(zwayApiHost);
            this.kodiBusinessService = new KodiService(kodiHost, 80, kodiUserName, kodiPassword);
        }

        private OventApiService apiService;

        private KodiService kodiBusinessService;
        /// <summary>
        /// Allume ou éteint la télé
        /// </summary>
        /// <param name="on">indique si l'on souhaite allumer ou éteindre</param>
        /// <returns></returns>
        public async Task AllumerLaTéléAsync(bool on) => await kodiBusinessService.AllumerLaTeleAsync(on);

        /// <summary>
        /// Lance le prochain épisode non vue de la série
        /// </summary>
        /// <param name="serieName">Nom de la série, tel que retourné par  @GetSeriesAsync</param>
        public async Task<bool> PlayNextEpisodeAsync(string serieName) => await kodiBusinessService.PlayNextEpisodeAsync(serieName);

        /// <summary>
        /// Toggle le mute sur Kodi
        /// </summary>
        public async Task MuteKodi() => await kodiBusinessService.Mute();

        /// <summary>
        /// Met la lecture en pause ou reprend la lecture en cours (toogle)
        /// </summary>
        public async Task PauseKodi() => await kodiBusinessService.Pause();

        /// <summary>
        /// Stop la lecture en cours sur Kodi
        /// </summary>
        public async Task StopKodi() => await kodiBusinessService.Stop();

        /// <summary>
        /// Avance dans le lecture en cours
        /// </summary>
        /// <param name="seconds">nombre de secondes à avancer</param>
        public async Task SeekKodi(int seconds) => await kodiBusinessService.Seek(seconds);

        /// <summary>
        /// Set le volume de Kodi
        /// </summary>
        /// <param name="volume">Volume à set (de 0 à 100)</param>
        public async Task SetKodiVolumn(int volume) => await kodiBusinessService.Volume(volume);



        /// <summary>
        /// Retourne la liste des films disponible sur le media center
        /// </summary>
        /// <returns>La liste des films disponible sur le media center</returns>
        public async Task<List<KodiRpc.Video.Details.Movie>> GetMoviesAsync() => await kodiBusinessService.GetMoviesAsync();

        /// <summary>
        /// Lance le film sur la télé
        /// </summary>
        /// <param name="movieName">nom du film, tel que retourné par @GetMoviesAsync</param>
        /// <returns></returns>
        public async Task<bool> PlayMovie(string movieName) => await kodiBusinessService.PlayMovieAsync(movieName);

        /// <summary>
        /// Retourne la liste des séries disponible sur le media center
        /// </summary>
        /// <returns>La liste des séries disponible sur le media center</returns>
        public async Task<List<KodiRpc.Video.Details.TVShow>> GetSeriesAsync() => await kodiBusinessService.GetSeriesAsync();

        /// <summary>
        /// Allume ou éteint la sono
        /// </summary>
        /// <returns></returns>
        public async Task PowerMusiqueAsync() => await apiService.AllumeEteinsLaMusiqueAsync();

        /// <summary>
        /// Lance la playlist selectionée sur la sono
        /// </summary>
        /// <param name="playlistNumber">Numéro de la playlist (1 à 6)</param>
        public async Task HandlePlaylistAsync(int playlistNumber)
        {
            switch (playlistNumber)
            {
                case 1: await apiService.SelectionneLePreset1Async(); break;
                case 2: await apiService.SelectionneLePreset2Async(); break;
                case 3: await apiService.SelectionneLePreset3Async(); break;
                case 4: await apiService.SelectionneLePreset4Async(); break;
                case 5: await apiService.SelectionneLePreset5Async(); break;
                case 6: await apiService.SelectionneLePreset6Async(); break;
            }
        }

        /// <summary>
        /// Allume ou éteint la lumière selectionée
        /// </summary>
        /// <param name="command">"on" ou "off"</param>
        /// <param name="room">"PC","TV","salon"</param>
        public async Task HandleLightsAsync(string command, string room)
        {
            switch (command)
            {
                case "on": await HandleLightOnAsync(room); break;
                case "off": await HandleLightOffAsync(room); break;
            }
        }

        /// <summary>
        /// monte ou descent le volet selectioné
        /// </summary>
        /// <param name="command">"on" ou "off"</param>
        /// <param name="room">"PC","TV","salon"</param>
        public async Task HandleStoresAsync(string command, string room)
        {
            switch (command)
            {
                case "on": await HandleStoreUpAsync(room); break;
                case "off": await HandleStoreDownAsync(room); break;
            }
        }

        private async Task HandleLightOnAsync(string room)
        {
            switch (room)
            {
                case "salon": await apiService.AllumerLaLumiereDuSalonAsync(); break;
                case "pc": await apiService.AllumerLaLumiereDuPCAsync(); break;
                case "tv": await apiService.AllumerLaLumiereTVAsync(); break;
            }
        }

        private async Task HandleLightOffAsync(string room)
        {
            switch (room)
            {
                case "salon": await apiService.EteindreLaLumiereDuSalonAsync(); break;
                case "pc": await apiService.EteindreLaLumiereDuPCAsync(); break;
                case "tv": await apiService.EteindreLaLumiereTVAsync(); break;
            }
        }

        private async Task HandleStoreUpAsync(string room)
        {
            switch (room)
            {
                case "salon": await apiService.OuvrirLesVoletsAsync(); break;
                case "pc": await apiService.OuvrirVoletPCAsync(); break;
                case "tv": await apiService.OuvrirVoletTVAsync(); break;
            }
        }

        private async Task HandleStoreDownAsync(string room)
        {
            switch (room)
            {
                case "salon": await apiService.FermerLesVoletsAsync(); break;
                case "pc": await apiService.FermerVoletPCAsync(); break;
                case "tv": await apiService.FermerVoletsTVAsync(); break;
            }
        }
    }
}

using KodiRpc;
using KodiRpc.Global;
using KodiRpc.List;
using KodiRpc.Video.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OventService
{
    public class KodiService
    {
        private Client kodiClient;

        private static (DateTime update, List<KodiRpc.Video.Details.TVShow> list) series;

        private static (DateTime update,List<KodiRpc.Video.Details.Movie> list) movies;

        public KodiService(string hostname, int port, string username = "", string password = "")
        {
            kodiClient = new Client(new ConnectionSettings(hostname, port, username, password), new PlatformServices());
        }

        public async Task<List<KodiRpc.Video.Details.TVShow>> GetSeriesAsync()
        {
            if (series.list != null && DateTime.Now - series.update < TimeSpan.FromDays(1))
            {
                return series.list;
            }
            var response = await kodiClient.VideoLibrary.GetTVShows(new TVShow { TVShowItem.title, TVShowItem.originaltitle, TVShowItem.episodeguide, TVShowItem.lastplayed }, new Limits { start = 0, end = 10000 }, new Sort { method = Sort_method.title, ignorearticle = true, order = Sort_order.ascending });
            series.update = DateTime.Now;
            return series.list = response.tvshows;
        }

        public async Task<List<KodiRpc.Video.Details.Movie>> GetMoviesAsync()
        {
            if (movies.list != null && DateTime.Now - movies.update < TimeSpan.FromDays(1))
            {
                return movies.list;
            }
            var response = await kodiClient.VideoLibrary.GetMovies(new Movie { MovieItem.title, MovieItem.originaltitle }, new Limits { start = 0, end = 10000 }, new Sort { method = Sort_method.title, ignorearticle = true, order = Sort_order.ascending });
            movies.update = DateTime.Now;
            return movies.list = response.movies;
        }

        public async Task Pause()
        {
            await kodiClient.Player.PlayPause(1);
        }

        public async Task Stop()
        {
            await kodiClient.Player.Stop(1);
        }

        public async Task Seek(int seconds)
        {
            await kodiClient.Player.Seek(1, new KodiRpc.Player.Seek_valueSeconds { seconds = seconds });
        }

        public async Task ForwardSmall()
        {
            await kodiClient.Player.Seek(1, KodiRpc.Player.Seek_value1.smallforward);
        }

        public async Task BackwardSmall()
        {
            await kodiClient.Player.Seek(1, KodiRpc.Player.Seek_value1.smallbackward);
        }

        public async Task ForwardBig()
        {
            await kodiClient.Player.Seek(1, KodiRpc.Player.Seek_value1.bigforward);
        }

        public async Task BackwardBig()
        {
            await kodiClient.Player.Seek(1, KodiRpc.Player.Seek_value1.bigbackward);
        }

        public async Task Mute()
        {
            await kodiClient.Application.SetMute(KodiRpc.Global.Toggle2.toggle);
        }
        public async Task Volume(int volume)
        {
            await kodiClient.Application.SetVolume(volume);
        }

        public async Task<bool> PlayMovieAsync(string movieName)
        {
            string lowerMovieName = movieName.ToLower();
            var movies = await GetMoviesAsync();
            var movie = movies.FirstOrDefault(x => x.originaltitle.ToLower().Contains(lowerMovieName) || x.title.ToLower().Contains(lowerMovieName));
            if (movie == null) return false;
            await kodiClient.Playlist.Clear(1);
            await kodiClient.Playlist.Insert(1, 0, new KodiRpc.Playlist.ItemMovieid { movieid = movie.movieid });
            await kodiClient.Player.Open(new KodiRpc.Player.Open_item1 { playlistid = 1, position = 0 });
            return true;
        }

        public async Task<bool> PlayRandomMovieAsync()
        {
            var movies = await GetMoviesAsync();
            var movie = movies[new Random().Next(movies.Count)];
            await kodiClient.Playlist.Clear(1);
            await kodiClient.Playlist.Insert(1, 0, new KodiRpc.Playlist.ItemMovieid { movieid = movie.movieid });
            await kodiClient.Player.Open(new KodiRpc.Player.Open_item1 { playlistid = 1, position = 0 });
            return true;
        }

        public async Task<bool> PlayNextEpisodeAsync(string serieName)
        {
            string lowerSerieName = serieName.ToLower();
            var series = await GetSeriesAsync();
            var serie = series.FirstOrDefault(x => x.label.ToLower().Contains(lowerSerieName) || x.title.ToLower().Contains(lowerSerieName));
            if (serie == null) return false;
            var episode = await kodiClient.VideoLibrary.GetEpisodes(
                serie.tvshowid,
                null,
                new Episode { EpisodeItem.title },
                new Limits { end = 1, start = 0 },
                new Sort { ignorearticle = true, method = Sort_method.date, order = Sort_order.ascending },
                new KodiRpc.List.Filter.Rule.Episodes { field = KodiRpc.List.Filter.Fields.Episodes.playcount, Operator = KodiRpc.List.Filter.Operators.Is, value = "0" }
            );
            if ((episode.episodes?.Count ?? 0 )== 0) return false;
            await kodiClient.Playlist.Clear(1);
            await kodiClient.Playlist.Insert(1, 0, new KodiRpc.Playlist.ItemEpisodeid { episodeid = episode.episodes[0].episodeid });
            await kodiClient.Player.Open(new KodiRpc.Player.Open_item1 { playlistid = 1, position = 0 });
            return true;
        }

        public async Task<bool> PlayLastShowAsync()
        {
            var series = await GetSeriesAsync();
            var last = series.OrderByDescending(x => x.lastplayed).FirstOrDefault();
            return await PlayNextEpisodeAsync(last.title);
        }

        public async Task<bool> PlayLatestEpisodeAsync(string serieName)
        {
            string lowerSerieName = serieName.ToLower();
            var series = await GetSeriesAsync();
            var serie = series.FirstOrDefault(x => x.label.ToLower().Contains(lowerSerieName) || x.title.ToLower().Contains(lowerSerieName));
            if (serie == null) return false;
            var episode = await kodiClient.VideoLibrary.GetEpisodes(
                serie.tvshowid,
                null,
                new Episode { EpisodeItem.title },
                new Limits { end = 1, start = 0 },
                new Sort { ignorearticle = true, method = Sort_method.date, order = Sort_order.descending }
            );
            if ((episode.episodes?.Count ?? 0) == 0) return false;
            await kodiClient.Playlist.Clear(1);
            await kodiClient.Playlist.Insert(1, 0, new KodiRpc.Playlist.ItemEpisodeid { episodeid = episode.episodes[0].episodeid });
            await kodiClient.Player.Open(new KodiRpc.Player.Open_item1 { playlistid = 1, position = 0 });
            return true;
        }

        public async Task<bool> PlayEpisodeAsync(string serieName, int season, int episodeNumber)
        {
            string lowerSerieName = serieName.ToLower();
            var series = await GetSeriesAsync();
            var serie = series.FirstOrDefault(x => x.label.ToLower().Contains(lowerSerieName) || x.title.ToLower().Contains(lowerSerieName));
            if (serie == null) return false;
            var episode = await kodiClient.VideoLibrary.GetEpisodes(
                serie.tvshowid,
                season,
                new Episode { EpisodeItem.title },
                new Limits { end = 1, start = 0 },
                new Sort { ignorearticle = true, method = Sort_method.date, order = Sort_order.descending },
                new KodiRpc.List.Filter.Rule.Episodes { field = KodiRpc.List.Filter.Fields.Episodes.episode, Operator = KodiRpc.List.Filter.Operators.Is, value = episodeNumber.ToString() }
            );
            if ((episode.episodes?.Count ?? 0) == 0) return false;
            await kodiClient.Playlist.Clear(1);
            await kodiClient.Playlist.Insert(1, 0, new KodiRpc.Playlist.ItemEpisodeid { episodeid = episode.episodes[0].episodeid });
            await kodiClient.Player.Open(new KodiRpc.Player.Open_item1 { playlistid = 1, position = 0 });
            return true;
        }

        public async Task AllumerLaTeleAsync(bool on)
        {
            await kodiClient.Addons.ExecuteAddon("script.json-cec", new { command = on ? "activate" : "standby" });
        }

        public async Task<IEnumerable<string>> GetLastAddedSeries()
        {
            var response = await kodiClient.VideoLibrary.GetEpisodes(null,null,new Episode { EpisodeItem.showtitle, EpisodeItem.dateadded },limits: new Limits {end = 10, start= 0},sort: new Sort { method= Sort_method.dateadded, order = Sort_order.descending });
            return response.episodes.Where(x => DateTime.Parse(x.dateadded).Date == DateTime.Now.Date).Select(x => x.showtitle);
        }

        public async Task Right()
        {
            await kodiClient.Input.Right();
        }
        public async Task Left()
        {
            await kodiClient.Input.Left();
        }
        public async Task Up()
        {
            await kodiClient.Input.Up();
        }
        public async Task Down()
        {
            await kodiClient.Input.Down();
        }
        public async Task Select()
        {
            await kodiClient.Input.Select();
        }
        public async Task Back()
        {
            await kodiClient.Input.Back();
        }
        public async Task Home()
        {
            await kodiClient.Input.Home();
        }
        public async Task Info()
        {
            await kodiClient.Input.Info();
        }
        public async Task Menu()
        {
            await kodiClient.Input.ContextMenu();
        }
        public async Task PageUp()
        {
            await kodiClient.Input.ExecuteAction(KodiRpc.Input.Action.pageup);
        }
        public async Task PageDown()
        {
            await kodiClient.Input.ExecuteAction(KodiRpc.Input.Action.pagedown);
        }
        public async Task FullScreen()
        {
            await kodiClient.GUI.SetFullscreen(Toggle2.toggle);
        }
        public async Task VolumeUp()
        {
            await kodiClient.Input.ExecuteAction(KodiRpc.Input.Action.volumeup);
        }
        public async Task VolumeDown()
        {
            await kodiClient.Input.ExecuteAction(KodiRpc.Input.Action.volumedown);
        }

        public async Task SubtitleOn()
        {
            await kodiClient.Player.SetSubtitle(0, true);
        }

        public async Task SubtitleOff()
        {
            await kodiClient.Player.SetSubtitle(0, false);
        }
        public async Task SubtitleNext()
        {
            await kodiClient.Player.SetSubtitle(0, KodiRpc.Player.SetSubtitle_subtitle1.next, null);
        }
        public async Task SubtitlePrevious()
        {
            await kodiClient.Player.SetSubtitle(0, KodiRpc.Player.SetSubtitle_subtitle1.previous, null);
        }
        public async Task SubtitleDownload()
        {
            await kodiClient.GUI.ActivateWindow(KodiRpc.GUI.Window.subtitlesearch);
        }

        public async Task AudioStreamNext()
        {
            await kodiClient.Player.SetAudioStream(0, KodiRpc.Player.SetAudioStream_stream1.next);
        }
        public async Task AudioStreamPrevious()
        {
            await kodiClient.Player.SetAudioStream(0, KodiRpc.Player.SetAudioStream_stream1.previous);
        }
        public async Task ViewMovies()
        {
            await kodiClient.GUI.ActivateWindow(KodiRpc.GUI.Window.videos, new List<string> { "MovieTitles" });
        }
        public async Task ViewShows()
        {
            await kodiClient.GUI.ActivateWindow(KodiRpc.GUI.Window.videos, new List<string> { "TvShowTitles" });
        }
        public async Task ViewMoviePlaylistRecent()
        {
            await kodiClient.GUI.ActivateWindow(KodiRpc.GUI.Window.videos, new List<string> { "RecentlyAddedMovies" });
        }
        public async Task ViewEpisodePlaylistRecent()
        {
            await kodiClient.GUI.ActivateWindow(KodiRpc.GUI.Window.videos, new List<string> { "RecentlyAddedEpisodes" });
        }
        public async Task Hibernate()
        {
            await kodiClient.System.Hibernate();
        }
        public async Task Reboot()
        {
            await kodiClient.System.Reboot();
        }
        public async Task Shutdown()
        {
            await kodiClient.System.Shutdown();
        }
        public async Task Suspend()
        {
            await kodiClient.System.Suspend();
        }
    }


}

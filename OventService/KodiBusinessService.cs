﻿using KodiRpc;
using KodiRpc.List;
using KodiRpc.Video.Fields;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OventService
{
    internal class KodiService
    {
        private Client kodiClient;

        private List<KodiRpc.Video.Details.TVShow> series;

        private List<KodiRpc.Video.Details.Movie> movies;

        public KodiService(string hostname,int port,string username = "", string password ="")
        {
            kodiClient =  new Client(new ConnectionSettings(hostname, port, username, password), new PlatformServices());
        }

        public async Task<List<KodiRpc.Video.Details.Movie>> GetMoviesAsync()
        {
            if (movies != null)
            {
                return movies;
            }
            var response = await kodiClient.VideoLibrary.GetMovies(new Movie { MovieItem.title, MovieItem.originaltitle }, new Limits { start = 0, end = 10000 }, new Sort { method = Sort_method.title, ignorearticle = true, order = Sort_order.ascending });
            return movies = response.movies;
        }


        public async Task<List<KodiRpc.Video.Details.TVShow>> GetSeriesAsync()
        {
            if (series != null)
            {
                return series;
            }
            var response = await kodiClient.VideoLibrary.GetTVShows(new TVShow { TVShowItem.title, TVShowItem.originaltitle, TVShowItem.episodeguide }, new Limits { start = 0, end = 10000 }, new Sort { method = Sort_method.title, ignorearticle = true, order = Sort_order.ascending });
            return series = response.tvshows;
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

        public async Task<bool> PlayNextEpisodeAsync(string serieName)
        {
            string lowerSerieName = serieName.ToLower();
            var series = await GetSeriesAsync();
            var serie = series.FirstOrDefault(x => x.originaltitle.ToLower().Contains(lowerSerieName) || x.title.ToLower().Contains(lowerSerieName));
            var episode = await kodiClient.VideoLibrary.GetEpisodes(
                serie.tvshowid,
                null,
                new Episode { EpisodeItem.title },
                new Limits { end = 1, start = 0 },
                new Sort { ignorearticle = true, method = Sort_method.date, order = Sort_order.ascending },
                new KodiRpc.List.Filter.Rule.Episodes { field = KodiRpc.List.Filter.Fields.Episodes.playcount, Operator = KodiRpc.List.Filter.Operators.Is, value = "0" }
            );
            if (episode.episodes?.Count == 0) return false;
            await kodiClient.Playlist.Clear(1);
            await kodiClient.Playlist.Insert(1, 0, new KodiRpc.Playlist.ItemEpisodeid { episodeid = episode.episodes[0].episodeid });
            await kodiClient.Player.Open(new KodiRpc.Player.Open_item1 { playlistid = 1, position = 0 });
            return true;
        }

        public async Task AllumerLaTeleAsync(bool on)
        {
            await kodiClient.Addons.ExecuteAddon("script.json-cec", new { command = on?"activate": "standby" });
        }
        
    }


}

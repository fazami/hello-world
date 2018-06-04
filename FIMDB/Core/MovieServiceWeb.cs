using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using FIMDB.Helpers;
using FIMDB.Model;

namespace FIMDB.Core
{
    public class MovieServiceWeb : IDisposable
    {
        private WebClient _webClient;

        public MovieServiceWeb()
        {
            _webClient = new WebClient();
        }

        #region private methods
        private Movie getMovieInfoFromWeb(string title, int year)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return null;
            }

            var findByExactTitle = "http://www.omdbapi.com/?t={title}&y={year}&plot=short";
            var searchByKeyword = "http://www.omdbapi.com/?s={searchKeyword}/";

            var yr = year == 0 ? "" : year.ToString();
            var url = findByExactTitle
                .Replace("{title}", title)
                .Replace("{year}", yr);
            string webResult = _webClient.DownloadString(new Uri(url));
            var movie = mapResponseToMovie(webResult);
            //var response = JsonConvert.DeserializeObject<OMDBFindMovieResponse>(webResult);

            if (movie != null)
            {
                return movie;
            }

            webResult = _webClient.DownloadString(
                new Uri(findByExactTitle
                .Replace("{title}", title)
                .Replace("{year}", "")));
            movie = mapResponseToMovie(webResult);

            if (movie != null)
            {
                return movie;
            }

            return null;
        }

        private Movie mapResponseToMovie(string webResult)
        {
            var ch1 = ((char)2481).ToString();
            var result = new Movie();

            var splitted = webResult.Replace("\\\"", ch1).Trim('{', '}').Split('"');

            for (int i = 1; i < splitted.Length; i += 4)
            {
                var id = splitted[i];
                var value = splitted[i + 2].Replace(ch1, "\"");
                int j;
                float f;

                switch (id)
                {
                    case "Title":
                        result.Title = value;
                        break;
                    case "Year":
                        result.Year = int.Parse(value);
                        break;
                    case "Rated":
                        result.Rated = value;
                        break;
                    case "Released":
                        break;
                    case "Runtime":
                        break;
                    case "Genre":
                        result.Genres = value.Split(',').ToList();
                        break;
                    case "Director":
                        result.Directors = value.Split(',').ToList();
                        break;
                    case "Writer":
                        result.Writers = value.Split(',').ToList();
                        break;
                    case "Actors":
                        result.Actors = value.Split(',').ToList();
                        break;
                    case "Language":
                        result.Languages = value.Split(',').ToList();
                        break;
                    case "Country":
                        result.Countries = value.Split(',').ToList();
                        break;
                    case "Awards":
                        result.Awards = value.Split(',').ToList();
                        break;
                    case "Poster":
                        result.PosterAddressOnWeb = value;
                        break;
                    case "Metascore":
                        result.MetaScore = int.TryParse(value, out j) ? j : 0;
                        break;
                    case "imdbRating":
                        result.ImdbRating = float.TryParse(value, out f) ? f : 0;
                        break;
                    case "imdbVotes":
                        result.ImdbTotalVotes = int.TryParse(value.Replace(",", ""), out j) ? j : 0;
                        break;
                    case "imdbID":
                        result.ImdbId = value;
                        break;
                    case "Response":
                        if (value == "false")
                            return null;
                        break;
                }
            }

            return result;
        }

        private Movie mapToMovie(OMDBFindMovieResponse response)
        {
            var result = new Movie
            {
                Actors = response.Actors.Split(',').ToList(),
                Awards = response.Awards.Split(',').ToList(),
                Directors = response.Director.Split(',').ToList(),
                Genres = response.Genre.Split(',').ToList(),
                ImdbId = response.imdbID,
                ImdbRating = float.Parse(response.imdbRating),
                ImdbTotalVotes = int.Parse(response.imdbVotes),
                Title = response.Title,
                Languages = response.Language.Split(',').ToList(),
                PosterAddressOnWeb = response.Poster,
                Writers = response.Writer.Split(',').ToList(),
                Year = response.Year
            };

            return result;
        }

        private void getCluesByFileName(string movieFileName, out string title, out int year)
        {
            var fileName = Path.GetFileNameWithoutExtension(movieFileName);
            var fileNameSplitted = fileName.Split(
                new char[] { '.', ' ', '_', '-', '(', ')', '{', '}', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);

            year = 0;
            title = "";
            var thisYear = DateTime.Today.Year;
            var yrIndex = 0;

            for (int i = 0; i < fileNameSplitted.Length; i++)
            {
                if (int.TryParse(fileNameSplitted[i], out year) &&
                    year > 1800 && year <= thisYear)
                {
                    yrIndex = i;
                    break;
                }

                title += fileNameSplitted[i] + " ";
            }

            title = title.Trim();

        }

        #endregion

        #region public methods
        public Movie FindMovieInWebByFileName(string movieFileName)
        {
            string movieName;
            int year;

            var file = new FileInfo(movieFileName);
            var size = file.Length;

            getCluesByFileName(movieFileName, out movieName, out year);
            var result = getMovieInfoFromWeb(movieName, year);
            result.FullFileName = movieFileName;
            result.FileSize = size;

            return result;
        }
        public List<Movie> FindTop250Movies()
        {
            var result = new List<Movie>();
            var top250 = loadTop250();
            foreach (var item in top250)
            {
                result.Add(getMovieInfoFromWeb(item.Key, item.Value));
            }
            return result;
        }

        private void updateMovieData(Movie item)
        {
            var m = getMovieInfoFromWeb(item.Title, item.Year);
        }

        private static List<KeyValuePair<string, int>> loadTop250()
        {
            var result = new List<KeyValuePair<string, int>>();
            int i1, i2, i3, i4, i5, i6 = 0;
            string c1, c2, c3, c4, c5, c6;
            var rawData = WebReader.ReadByUrl("http://www.imdb.com/chart/top");

            c1 = "<td class=\"titleColumn\">";
            c2 = "title=\"";
            c3 = ">";
            c4 = "</";
            c5 = "<span class=\"secondaryInfo\">(";
            c6 = ")</";

            while (true)
            {
                i1 = rawData.IndexOf(c1, i6);
                if (i1 == -1)
                    break;
                i1 += c1.Length;
                i2 = rawData.IndexOf(c2, i1) + c2.Length;
                i3 = rawData.IndexOf(c3, i2) + c3.Length;
                i4 = rawData.IndexOf(c4, i3);
                var name = rawData.Substring(i3, i4 - i3);
                i5 = rawData.IndexOf(c5, i4) + c5.Length;
                i6 = rawData.IndexOf(c6, i5);
                var year = rawData.Substring(i5, i6 - i5);
                result.Add(new KeyValuePair<string, int>(name, int.Parse(year)));
            }
            return result;

        }

        public void Dispose()
        {
            try
            {
                _webClient.Dispose();
            }
            catch { }
        }

        #endregion
    }
}

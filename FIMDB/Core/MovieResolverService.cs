using FIMDB.DAL;
using FIMDB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMDB.Core
{
    class MovieResolverService
    {
        char[] _invalidChars = new[] { '.', '/', '\\', '\"', '\'', '(', ')', '[', ']', ',', ';', ' ', '_', '-', '{', '}' };
        List<MovieBrief> _allMovies;
        Dictionary<MovieBrief, string[]> _movieNamesSplitted;
        FimdbRepository _movieRepository;

        public MovieResolverService()
        {
            _movieRepository = new FimdbRepository();
            _allMovies = _movieRepository.GetMovieBriefList();
            _movieNamesSplitted = getMovieNamesSplitted();

        }

        private Dictionary<MovieBrief, string[]> getMovieNamesSplitted()
        {
            var result = new Dictionary<MovieBrief, string[]>();
            foreach (var m in _allMovies)
            {
                string title = m.Title.ToLower();
                if (title.StartsWith("the "))
                    title = m.Title.Substring(4);
                var splitted = title.Split(_invalidChars, StringSplitOptions.RemoveEmptyEntries);
                result.Add(m, splitted);
            }
            return result;
        }

        private void getCluesByFileName(string movieFileName, out string title, out int year)
        {
            var fileName = removeExtension(movieFileName);
            var fileNameSplitted = fileName.Split(_invalidChars, StringSplitOptions.RemoveEmptyEntries);

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

                if (fileNameSplitted[i].StartsWith("720p") ||
                    fileNameSplitted[i].StartsWith("480p") ||
                    fileNameSplitted[i].StartsWith("1080p") ||
                    fileNameSplitted[i] == "bdrip" ||
                    fileNameSplitted[i] == "dvdrip" ||
                    fileNameSplitted[i] == "xvid" ||
                    fileNameSplitted[i] == "subs")
                    break;

                title += fileNameSplitted[i] + " ";
            }

            title = title.Trim();
        }

        private string removeExtension(string movieFileName)
        {
            var result = movieFileName.Remove(movieFileName.LastIndexOf('.'));
            return result;
        }
        //private ResolveResult tryResolveLevel3(string title, int year)
        //{
        //    var result = new ResolveResult();
        //    var splitedSearchTitle = title.Split(_invalidChars, StringSplitOptions.RemoveEmptyEntries);
        //    List<KeyValuePair<MovieBrief, int>> movies1 = new List<KeyValuePair<MovieBrief, int>>();
        //    var charCount = splitedSearchTitle.Select(a => a.Length).Sum();
        //    foreach (var movie in _allMovies)
        //    {
        //        var splitedTitle = movie.Title.Split(_invalidChars, StringSplitOptions.RemoveEmptyEntries);
        //        var simmilarWords = splitedSearchTitle.Where(st => splitedTitle.Contains(st)).ToList();
        //        var cnt = simmilarWords.Select(a => a.Length).Sum();
        //        if (cnt * 100 / charCount > 75)
        //            movies1.Add(new KeyValuePair<MovieBrief, int>(movie, cnt));
        //    }
        //    movies1 = movies1.Where(a => a.Value == movies1.Max(m1 => m1.Value)).ToList();
        //    if (year != 0)
        //        movies1 = movies1.Where(a => a.Key.Year == year).ToList();

        //    if (movies1.Count == 1)
        //    {
        //        result.Movies = new List<MovieBrief> { movies1[0].Key };
        //        result.Probability = 80;
        //    }
        //    else if (movies1.Count > 0)
        //    {
        //        result.Movies = new List<MovieBrief>();
        //        result.Movies.AddRange(movies1.Select(a => a.Key));
        //        result.Probability = 80 / movies1.Count();
        //    }
        //    return result;
        //}

        private ResolveResult tryResolveLevel2(string title, int year)
        {
            var result = new ResolveResult();
            if (title.StartsWith("the "))
                title = title.Substring(4);
            var titleSplitted = title.Split(_invalidChars, StringSplitOptions.RemoveEmptyEntries);
            var movies1 = new List<MovieBrief>();
            bool found;
            for (int i = 0; i < _movieNamesSplitted.Count; i++)
            {
                found = true;
                var nameSplitted = _movieNamesSplitted.ElementAt(i).Value;
                for (int j = 0; j < titleSplitted.Length; i++)
                {
                    var namePart = nameSplitted.ElementAtOrDefault(i);
                    if (namePart != titleSplitted[i])
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                    break;
            }
            var movies1 = _movieNamesSplitted.Where(a => a.Value == titleSplitted).ToList();
            if (year != 0)
                movies1 = movies1.Where(a => a.Key.Year == year).ToList();

            if (movies1.Count == 1)
            {
                result.Movies = new List<MovieBrief> { movies1[0].Key };
                result.Probability = 90;
            }
            else if (movies1.Count > 0)
            {
                result.Movies = new List<MovieBrief>();
                result.Movies.AddRange(movies1.Select(a => a.Key));
                result.Probability = 90 / movies1.Count();
            }
            return result;
        }

        private ResolveResult tryResolveLevel1(string title, int year)
        {
            var result = new ResolveResult();
            var moviesq = _allMovies.Where(a => a.Title.ToLower() == title);
            var movies1 = moviesq.ToList();
            if (year != 0)
                moviesq = moviesq.Where(a => a.Year == year);
            movies1 = moviesq.ToList();

            if (movies1.Count == 1)
            {
                result.Movies = new List<MovieBrief> { movies1[0] };
                result.Probability = 100;
            }
            else if (movies1.Count > 0)
            {
                result.Movies = new List<MovieBrief>();
                result.Movies.AddRange(movies1);
                result.Probability = 100 / movies1.Count();
            }

            return result;
        }

        public ResolveResult Resolve(string fileName)
        {
            fileName = fileName.Trim().ToLower();
            string title;
            int year;
            getCluesByFileName(fileName, out title, out year);

            if (string.IsNullOrEmpty(title))
                return null;

            var resolveResult = tryResolveLevel1(title, year);
            if (resolveResult.Success)
                return resolveResult;

            resolveResult = tryResolveLevel2(title, year);
            if (resolveResult.Success)
                return resolveResult;

            //resolveResult = tryResolveLevel3(title, year);
            //if (resolveResult.Success)
            //    return resolveResult;

            return null;
        }

        //private void setToResult(ResolveResult resolveResult, MovieFile m)
        //{
        //    if (resolveResult.Movies.Count == 1)
        //    {
        //        m.MovieId = resolveResult.Movies[0].Id;
        //        m.ImdbId = resolveResult.Movies[0].ImdbId;
        //        m.Title = resolveResult.Movies[0].Title;
        //        m.ProbableImdbIds = resolveResult.Movies[0].ImdbId;
        //        m.Probability = resolveResult.Probability;
        //    }
        //    else if (resolveResult.Movies.Any())
        //    {
        //        var imdbIds = resolveResult.Movies.Select(a => a.ImdbId).ToList();
        //        m.ProbableImdbIds = string.Join(",", imdbIds);
        //        m.Probability = resolveResult.Probability;
        //    }

        //}
    }
}

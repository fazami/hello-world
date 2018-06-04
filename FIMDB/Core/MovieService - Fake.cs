using FIMDB.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;

namespace FIMDB.Core
{
    public class MovieService : IDisposable
    {
        MovieFileManager _movieFileManager;
        MovieDBRepository _movieDBRepository;

        public MovieService()
        {
            //_movieFileManager = new MovieFileManager();
            //_movieDBRepository = new MovieDBRepository();
        }

        public List<Movie> GetAllMoviesInDatabase()
        {
            var allMovies = getAllMovies();

            var movieExtensions = new[] { ".mkv", ".avi", ".mp4", ".vob" };
            var myMoviesFiles = File.ReadAllLines("d:\\movies.log");
            var myMoviesFileNames = File.ReadAllLines("d:\\movies.log")
                .Where(a => movieExtensions.Contains(Path.GetExtension(a).ToLower()))
                .Select(a => Path.GetFileName(a).ToLower())
                .ToList();

            var result = new List<Movie>();
            foreach (var m in myMoviesFileNames)
            {
                string title;
                int year;
                getCluesByFileName(m, out title, out year);
                var xname = allMovies.FirstOrDefault(a => string.Compare(a.Title, title) == 0);
                //if(xname==null)
                //    xname = allMovies.FirstOrDefault(a=>a.Title.ToLower().Contains()

                result.Add(new Movie
                {
                    FullFileName = m,
                    Title = title,
                    Year = year
                });
            }

            return allMovies;
        }
        //private List<Movie> searchMovie(List<Movie> allMovies, string title, int year)
        //{
        //    var m = allMovies.Where(a => string.Compare(a.Title, title) == 0);
        //    if (m != null)
        //        return m.ToList();


        //}
        private List<Movie> getAllMovies()
        {
            var result = new List<Movie>();
            var file = "d:\\imdbmovies.log";
            var lines = File.ReadAllLines(file);
            foreach (var item in lines)
            {
                var split = item.Split('|');
                var imdbId = split[0];
                var title = split[1];
                var year = split[2];
                var rate = split[3];
                var directors = split[4];
                var stars = split[5];
                var genre = split[6];
                var vote = split[7];
                var gross = split[7];

                var i = year.IndexOf("(19");
                if (i == -1)
                    i = year.IndexOf("(20");
                int y = 0;
                if (i != -1)
                    y = int.Parse(year.Substring(i + 1, 4));

                var movie = new Movie
                {
                    //Actors = stars.Split(';').ToList(),
                    ActorsFormatted = stars,
                    //Directors = directors.Split(';').ToList(),
                    DirectorsFormatted = directors,
                    //Genres = genre.Split(',').ToList(),
                    GenresFormatted = genre,
                    ImdbId = imdbId,
                    ImdbRating = rate == "" ? 0 : float.Parse(rate),
                    ImdbTotalVotes = vote == "" ? 0 : int.Parse(vote),
                    Title = title,
                    Year = y,
                    Gross = gross == "" ? (float?)null : float.Parse(gross),
                };
                result.Add(movie);
            }

            return result;
        }

        public List<string> GetAllMoviesInDatabase1()
        {
            return null;
            //var movieExtensions = new[] { ".mkv", ".avi", ".mp4", ".vob" };
            //var myMovies = File.ReadAllLines("d:\\movies.log")
            //    .Where(a => movieExtensions.Contains(Path.GetExtension(a).ToLower()))
            //    .Select(a => Path.GetFileName(a).ToLower())
            //    .ToList();
            //return myMovies;

            //List<Movie> result = new List<Movie>();
            //foreach (var m in myMovies)
            //{
            //    string title;
            //    int year;
            //    getCluesByFileName(m, out title, out year);
            //    result.Add(new Movie
            //    {
            //        FullFileName = m,
            //        Title = title,
            //        Year = year
            //    });
            //}
            //return result;
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

                if (fileNameSplitted[i].StartsWith("720p") ||
                    fileNameSplitted[i].StartsWith("480p") ||
                    fileNameSplitted[i].StartsWith("1080p"))
                    break;

                title += fileNameSplitted[i] + " ";
            }

            title = title.Trim();

        }

        public List<Movie> GetAllMoviesInDirectory(string dirName)
        {
            var movieFileInfo = _movieFileManager.GetAllMoviesInDirectory(dirName);
            var movies = _movieDBRepository.GetMoviesInDirectory(dirName);
            foreach (var item in movieFileInfo)
            {
                var m = movies.FirstOrDefault(a => string.Compare(a.FullFileName, item.FileName, true) == 0);
                m.Poster = item.Poster;
                m.FileSize = item.Size;
            }

            return movies;
        }
        public void AddMovies(List<Movie> movies)
        {
            foreach (var movie in movies)
            {
                AddMovie(movie);
            }
        }
        public void AddMovie(Movie movie)
        {
            if (_movieDBRepository.MovieFileNameExists(movie.FullFileName))
            {
                return;
            }

            var movieFileInfo = _movieFileManager.GetMovieFileInfo(movie.FullFileName);
            using (var tran = new TransactionScope())
            {
                _movieDBRepository.AddMovie(movie);
                if (movie.Poster != null)
                {
                    _movieFileManager.UpdatePoster(movie.FullFileName, movie.Poster);
                }
                tran.Complete();
            }
        }
        public void UpdateMovies(List<Movie> movies)
        {
            foreach (var movie in movies)
            {
                UpdateMovie(movie);
            }
        }
        public void UpdateMovie(Movie movie)
        {
            var m = _movieDBRepository.GetMovieById(movie.Id);
            var origFullFileName = m.FullFileName;
            using (var tran = new TransactionScope())
            {
                _movieDBRepository.UpdateMovie(movie);
                if (origFullFileName != movie.FullFileName)
                {
                    _movieFileManager.UpdateMovieFileName(origFullFileName, movie.FullFileName);
                }
                if (movie.Poster != null)
                {
                    _movieFileManager.UpdatePoster(movie.FullFileName, movie.Poster);
                }
                tran.Complete();
            }
        }
        public void DeleteMovie(List<Movie> movies)
        {
            foreach (var movie in movies)
            {
                DeleteMovie(movie);
            }
        }
        public void DeleteMovie(Movie movie)
        {
            using (var tran = new TransactionScope())
            {
                _movieDBRepository.DeleteMovie(movie);
                _movieFileManager.DeleteMovieFile(movie.FullFileName);
                tran.Complete();
            }
        }
        public void Dispose()
        {
            try { _movieDBRepository.Dispose(); }
            catch { }
        }
    }
}

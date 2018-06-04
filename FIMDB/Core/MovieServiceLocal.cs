using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using FIMDB.DB.Files;
using FIMDB.DB.Sqlite;
using FIMDB.Model;

namespace FIMDB.Core
{
    public class MovieServiceLocal : IDisposable
    {
        MovieFileManager _movieFileManager;
        MovieDBRepository _movieDBRepository;

        public MovieServiceLocal()
        {
            _movieFileManager = new MovieFileManager();
            _movieDBRepository = new MovieDBRepository();
        }

        public List<Movie> GetAllMoviesInDatabase()
        {
            var movies = _movieDBRepository.GetAllMovies();
            var movieNames = movies.Select(a => a.FullFileName).ToList();
            var movieFileInfo = _movieFileManager.GetMovieInfos(movieNames);
            foreach (var item in movieFileInfo)
            {
                var m = movies.FirstOrDefault(a => string.Compare(a.FullFileName, item.FileName, true) == 0);
                m.Poster = item.Poster;
                m.FileSize = item.Size;
            }

            return movies;
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

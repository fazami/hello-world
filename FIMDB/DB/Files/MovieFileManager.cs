using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using FIMDB.Model;

namespace FIMDB.DB.Files
{
    public class MovieFileManager
    {
        public MovieFileModel GetMovieFileInfo(string movieFileName)
        {
            var f = new FileInfo(movieFileName);
            var posterFileName = getPosterFileNameOfMovie(movieFileName);
            var hasPoster = File.Exists(posterFileName);
            var result = new MovieFileModel
            {
                FileName = movieFileName,
                Size = f.Length,
                Poster = hasPoster ? File.ReadAllBytes(posterFileName) : null
            };
            return result;
        }
        public List<MovieFileModel> GetAllMoviesInDirectory(string dirName)
        {
            var dir = new DirectoryInfo(dirName);
            var files = dir
                .GetFiles("*", SearchOption.AllDirectories)
                .Where(a => a.Length > 100000000)
                .ToList();

            return files.Select(a => new MovieFileModel
            {
                FileName = a.FullName,
                Size = a.Length,
            }).ToList();
        }
        public List<MovieFileModel> GetMovieInfos(List<string> movieFileNames)
        {
            var result = new List<MovieFileModel>();
            foreach (var fileName in movieFileNames)
            {
                var file = new FileInfo(fileName);
                result.Add(new MovieFileModel
                {
                    FileName = fileName,
                    Size = file.Length
                });
            }
            return result;
        }

        public void UpdateMovieFileName(string oldMovieFullFileName, string newMovieFullFileName)
        {
            var updater = new TransactionalFileNameUpdater(oldMovieFullFileName, newMovieFullFileName);
            var oldPosterName = getPosterFileNameOfMovie(oldMovieFullFileName);
            if (File.Exists(oldPosterName))
            {
                var newPosterName  = getPosterFileNameOfMovie(newMovieFullFileName);
                var posterUpdater = new TransactionalFileNameUpdater(oldPosterName, newPosterName);
            }
        }

        public void DeleteMovieFile(string movieFullFileName)
        {
            var deleter = new TransactionalFileDeleter(movieFullFileName);
            DeletePoster(movieFullFileName);
        }

        public void AddPoster(string movieFullFileName, byte[] poster)
        {
            var posterFileName = getPosterFileNameOfMovie(movieFullFileName);
            var creator = new TransactionalFileCreator(posterFileName, poster);
        }

        public void UpdatePoster(string movieFullFileName, byte[] poster)
        {
            var posterFileName = getPosterFileNameOfMovie(movieFullFileName);
            var origPoster = File.ReadAllBytes(posterFileName);
            bool areSimilar = origPoster.Length == poster.Length;

            if (areSimilar)
            {
                for (int i = 0; i < origPoster.Length; i++)
                {
                    if (origPoster[i] != movieFullFileName[i])
                    {
                        areSimilar = false;
                        break;
                    }
                }
            }

            if (areSimilar == false)
            {
                DeletePoster(movieFullFileName);
                AddPoster(movieFullFileName, poster);
            }
        }

        public void DeletePoster(string movieFullFileName)
        {
            var posterFileName = getPosterFileNameOfMovie(movieFullFileName);
            var deleter = new TransactionalFileDeleter(posterFileName);
        }

        private string getPosterFileNameOfMovie(string movieFullFileName)
        {
            var posterFileName = Path.GetDirectoryName(movieFullFileName) + "\\" +
                Path.GetFileNameWithoutExtension(movieFullFileName) + ".jpg";
            return posterFileName;
        }


    }
}

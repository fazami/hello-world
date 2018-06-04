using FIMDB.DAL;
using FIMDB.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMDB.Core
{
    class MovieService:IDisposable
    {
        #region members
        const string movieExtensions = ".webm .mkv .flv .vob .ogv .ogg .avi .mov .qt .wmv .mp4 .m4p .m4v .mpg .mpeg .3gp .asf";
        FimdbRepository _movieRepository;

        #endregion
        #region constructors
        public MovieService()
        {
            _movieRepository = new FimdbRepository();
        }

        #endregion
        #region private methods
        private void fixDriveLetters(List<MovieFile> movieFiles)
        {
            var drives = Environment.GetLogicalDrives();
            var mfDrives = movieFiles.Select(a => a.DirName.Remove(3).ToUpper())
                .Distinct()
                .ToDictionary(a => a, b => (string)null);
            var rnd = new Random();
            foreach (var d in mfDrives.Keys.ToList())
            {
                var files = movieFiles
                    .OrderBy(a => rnd.Next())
                    .Where(a => a.DirName.StartsWith(d, StringComparison.InvariantCultureIgnoreCase))
                    .Take(15)
                    .Select(a => a.DirName.Substring(3) + "\\" + a.FileName)
                    .ToList();

                mfDrives[d] = findBestFitDriveForFiles(files, drives);
            }
            foreach (var d in mfDrives)
            {
                movieFiles.Where(a => a.DirName.StartsWith(d.Key, StringComparison.InvariantCultureIgnoreCase))
                    .ToList()
                    .ForEach(a =>
                    {
                        if (d.Value == null)
                        {
                            a.IsMissing = true;
                        }
                        else
                        {
                            a.IsMissing = false;
                            a.DirName = d.Value + a.DirName.Substring(3);
                        }
                    });
            }
        }

        private string findBestFitDriveForFiles(List<string> files, string[] logicalDrives)
        {
            int existCount;
            var sufficientCount = files.Count <= 5 ? files.Count : files.Count / 2;
            foreach (var d in logicalDrives)
            {
                existCount = files.Count(a => File.Exists(d + a));
                if (existCount >= sufficientCount)
                    return d;
            }

            return null;
        }

        #endregion
        #region public methods
        public List<MovieFile> GetMovieFileList()
        {
            var data = _movieRepository.GetMovieFileList();
            fixDriveLetters(data);
            return data;
        }
        public List<FoundMovieFile> SearchMoviesInHDD(string[] paths)
        {
            var exts = movieExtensions.Split(' ');
            var result = new List<FoundMovieFile>();
            foreach (var p in paths)
            {
                var dirInfo = new DirectoryInfo(p);
                var files = dirInfo.EnumerateFiles("*", SearchOption.AllDirectories)
                    .Where(a => exts.Any(b => a.Name.ToLower().EndsWith(b)) && a.Length > 30000000)
                    .Select(a => new FoundMovieFile
                    {
                        DirectoryName = a.DirectoryName,
                        FileName = a.Name.ToLower(),
                        Size = a.Length,
                    })
                    .ToList();
                result.AddRange(files);
            }
            return result;
        }
        public void UpdateSelectedPath(List<SearchPath> paths)
        {
            _movieRepository.UpdateSelectedPath(paths);
        }
        public List<SearchPath> GetSearchPaths()
        {
            return _movieRepository.GetSearchPaths();
        }
        public void Dispose()
        {
            _movieRepository.Dispose();
        }

        #endregion

    }
}

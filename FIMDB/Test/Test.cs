using FIMDB.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace FIMDB.Test
{
    public class Test
    {
        public static bool RunTest()
        {
            //return false;

            //findMovieByFileName();
            //TestTransactionScope();
            //TestSqlite();
            //TestDBContext();
            //TestReadWeb();
            //loadTop250();
            //findRedundentMovies();
            //findAllExtensions();
            //getIMDBData();
            //testGetListFromDataReader();

            return true;
        }

        private static void testGetListFromDataReader()
        {

        }



        private static void findRedundentMovies()
        {
            var movieExtensions = new[] { ".mkv", ".avi", ".mp4", ".vob" };
            var movies = File.ReadAllLines("d:\\movies.log")
                .Where(a => movieExtensions.Contains(Path.GetExtension(a).ToLower()))
                .GroupBy(a => Path.GetFileName(a).ToLower())
                .Select(a => new
                {
                    Movie = a.Key,
                    Count = a.Count()
                })
                .Where(a => a.Count > 1)
                .ToList();
        }

        private static void findAllExtensions()
        {
            var movieExtensions = new[] { ".mkv", ".avi", ".mp4", ".vob" };
            var movies = File.ReadAllLines("d:\\movies.log")
                .Where(a => movieExtensions.Contains(Path.GetExtension(a).ToLower()))
                .Select(a => Path.GetFileNameWithoutExtension(a).ToLower())
                .ToList();

            //var exts = movies.Select(a => Path.GetExtension(a).ToLower())
            //    .GroupBy(a => a)
            //    .Select(a => new
            //    {
            //        Extension = a.Key,
            //        Count = a.Count()
            //    })
            //    .OrderByDescending(a => a.Count)
            //    .ToList();

        }
        private static void getCluesByFileName(string movieFileName, out string title, out int year)
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

        private static void TestReadWeb()
        {
            var data = WebReader.ReadByUrl("http://www.imdb.com/chart/top");

        }



        private static void TestTransactionScope()
        {
            //var tx = new TxFileManager();
            //var fileName = "c:\\mytest.txt";
            //var bytes = File.ReadAllBytes(@"D:\Tutorials\Design Pattern Class\AOOD-Session-09-1395-04-27\ExtendProvider\TransactionEnlistMent\program.cs");
            //using (var tran = new TransactionScope())
            //{
            //    var mdb = new MovieDBRepository();
            //    var mfile = new MovieFileManager();

            //    mdb.UpdateMovie(new Movie());
            //    mfile.UpdateMovieFileName("", "");
            //    mfile.UpdatePoster("", null);
            //    tran.Complete();
            //}
        }
        private static void findMovieByFileName()
        {

        }
    }
}

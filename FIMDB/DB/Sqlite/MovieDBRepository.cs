using FIMDB.Helpers;
using FIMDB.Model;
using System;
using System.Collections.Generic;

namespace FIMDB.DB.Sqlite
{
    public class MovieDBRepository : IDisposable
    {
        private string _cnStr;
        private SQLiteConnection _cn;
        public MovieDBRepository()
        {
            _cnStr = @"Data Source=D:\My Documents\My Projects\Developement\Current\XIMDB\XIMDB\Database\MDB.db";
            _cn = new SQLiteConnection(_cnStr);
            _cn.Open();
        }
        public Movie GetMovieById(int id)
        {
            Movie result;

            using (var cmd = _cn.CreateCommand())
            {
                cmd.CommandText = "select Id,ImdbId,Title,Year,ImdbRating,ImdbTotalVotes,MetaScore,Rated,Genres,Actors,Directors,Writers,Languages,Awards,Countries,FullFileName from Movies where Id=" + id;
                using (var dr = cmd.ExecuteReader())
                {
                    dr.Read();
                    result = mapToMovie(dr);
                }
            }

            return result;
        }

        private Movie mapToMovie(SQLiteDataReader dr)
        {
            Movie result;

            result = new Movie
            {
                Id = (int)(long)dr["Id"],
                ImdbId = dr["ImdbId"] as string,
                Title = dr["Title"] as string,
                Year = (int)(dr["Year"] as long?).GetValueOrDefault(),
                ImdbRating = (float)(dr["ImdbRating"] as double?).GetValueOrDefault(),
                ImdbTotalVotes = (int)(dr["ImdbTotalVotes"] as long?).GetValueOrDefault(),
                MetaScore = (int)(dr["MetaScore"] as long?).GetValueOrDefault(),
                Rated = dr["Rated"] as string,
                GenresFormatted = dr["Genres"] as string,
                ActorsFormatted = dr["Actors"] as string,
                DirectorsFormatted = dr["Directors"] as string,
                WritersFormatted = dr["Writers"] as string,
                LanguagesFormatted = dr["Languages"] as string,
                AwardsFormatted = dr["Awards"] as string,
                CountriesFormatted = dr["Countries"] as string,
                FullFileName = dr["FullFileName"] as string
            };
            return result;
        }

        public List<Movie> GetAllMovies()
        {
            List<Movie> result = new List<Movie>();

            using (var cmd = _cn.CreateCommand())
            {
                cmd.CommandText = "select Id,ImdbId,Title,Year,ImdbRating,ImdbTotalVotes,MetaScore,Rated,Genres,Actors,Directors,Writers,Languages,Awards,Countries,FullFileName from Movies";
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result.Add(mapToMovie(dr));
                    }
                }
            }

            return result;
        }

        public List<Movie> GetMoviesInDirectory(string directoryName)
        {
            List<Movie> result = new List<Movie>();

            using (var cmd = _cn.CreateCommand())
            {
                cmd.CommandText = "select Id,ImdbId,Title,Year,ImdbRating,ImdbTotalVotes,MetaScore,Rated,Genres,Actors,Directors,Writers,Languages,Awards,Countries,FullFileName from Movies where FullFileName like '" + directoryName.Replace("'", "''") + "%'";
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result.Add(mapToMovie(dr));
                    }
                }
            }

            return result;
        }

        public void AddMovie(Movie movie)
        {
            var query = @"
insert into movies 
    (ImdbId,Title,Year,ImdbRating,ImdbTotalVotes,MetaScore,Rated,Genres,Actors,Directors,Writers,Languages,Awards,Countries,FullFileName) 
values 
    ({ImdbId},{Title},{Year},{ImdbRating},{ImdbTotalVotes},{MetaScore},{Rated},{Genres},{Actors},{Directors},{Writers},{Languages},{Awards},{Countries},{FullFileName});
select last_insert_rowid();"
                .ReplaceScript("{ImdbId}", movie.ImdbId)
                .ReplaceScript("{Title}", movie.Title)
                .ReplaceScript("{Year}", movie.Year)
                .ReplaceScript("{ImdbRating}", movie.ImdbRating)
                .ReplaceScript("{ImdbTotalVotes}", movie.ImdbTotalVotes)
                .ReplaceScript("{MetaScore}", movie.MetaScore)
                .ReplaceScript("{Rated}", movie.Rated)
                .ReplaceScript("{Genres}", movie.GenresFormatted)
                .ReplaceScript("{Actors}", movie.ActorsFormatted)
                .ReplaceScript("{Directors}", movie.DirectorsFormatted)
                .ReplaceScript("{Writers}", movie.WritersFormatted)
                .ReplaceScript("{Languages}", movie.LanguagesFormatted)
                .ReplaceScript("{Awards}", movie.AwardsFormatted)
                .ReplaceScript("{Countries}", movie.CountriesFormatted)
                .ReplaceScript("{FullFileName}", movie.FullFileName);

            using (var cmd = _cn.CreateCommand())
            {
                cmd.CommandText = query;
                movie.Id = (int)(long)cmd.ExecuteScalar();
            }
        }

        public void UpdateMovie(Movie movie)
        {
            var query = @"
update movies 
set ImdbId={ImdbId}
    ,Title={Title}
    ,Year={Year}
    ,ImdbRating={ImdbRating}
    ,ImdbTotalVotes={ImdbTotalVotes}
    ,MetaScore={MetaScore}
    ,Rated={Rated}
    ,Genres={Genres}
    ,Actors={Actors}
    ,Directors={Directors}
    ,Writers={Writers}
    ,Languages={Languages}
    ,Awards={Awards}
    ,Countries={Countries}
    ,FullFileName={FullFileName}
    where Id={Id}"
                .ReplaceScript("{Id}", movie.Id)
                .ReplaceScript("{ImdbId}", movie.ImdbId)
                .ReplaceScript("{Title}", movie.Title)
                .ReplaceScript("{Year}", movie.Year)
                .ReplaceScript("{ImdbRating}", movie.ImdbRating)
                .ReplaceScript("{ImdbTotalVotes}", movie.ImdbTotalVotes)
                .ReplaceScript("{MetaScore}", movie.MetaScore)
                .ReplaceScript("{Rated}", movie.Rated)
                .ReplaceScript("{Genres}", movie.GenresFormatted)
                .ReplaceScript("{Actors}", movie.ActorsFormatted)
                .ReplaceScript("{Directors}", movie.DirectorsFormatted)
                .ReplaceScript("{Writers}", movie.WritersFormatted)
                .ReplaceScript("{Languages}", movie.LanguagesFormatted)
                .ReplaceScript("{Awards}", movie.AwardsFormatted)
                .ReplaceScript("{Countries}", movie.CountriesFormatted)
                .ReplaceScript("{FullFileName}", movie.FullFileName);

            using (var cmd = _cn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteMovie(Movie movie)
        {
            var query = "delete movies where Id={Id}"
                .ReplaceScript("{Id}", movie.Id);

            using (var cmd = _cn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
            }
        }

        public bool MovieFileNameExists(string fileName)
        {
            bool result;
            using (var cmd = _cn.CreateCommand())
            {
                cmd.CommandText = "select Id from Movies where FullFileName = {FullFileName}"
                    .ReplaceScript("{FullFileName}", fileName);

                var r = cmd.ExecuteScalar();
                result = r != null;
            }
            return result;
        }

        public void Dispose()
        {
            try { _cn.Close(); }
            catch { }
        }

    }
}

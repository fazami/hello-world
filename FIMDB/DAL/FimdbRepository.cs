using Farzin.Infrastructure.CrossCutting.Helpers;
using Farzin.Infrastructure.DAL;
using FIMDB.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FIMDB.DAL
{
    class FimdbRepository : SqlBase
    {
        public FimdbRepository() : base("imdb")
        {
        }

        public void SaveMovieInfos(List<MovieInfo> movies)
        {
            var query = @"
update Movies set 
	Title={Title},
	Year={Year},
	YearEnd={YearEnd},
	Duration={Duration},
	Rating={Rating},
	Directors={Directors},
	Stars={Stars},
	Genre={Genre},
	Votes={Votes},
	Gross={Gross},
	MetaScore={MetaScore},
	MPAA={MPAA},
    MovieType={MovieType},
	FetchDate={FetchDate}
where ImdbId={ImdbId}

if(@@ROWCOUNT=0)
insert Movies
	(Id,ImdbId,Title,Year,YearEnd,Duration,Rating,Directors,Stars,Genre,Votes,Gross,MetaScore,MPAA,MovieType,FetchDate)
	values
	({Id},{ImdbId},{Title},{Year},{YearEnd},{Duration},{Rating},{Directors},{Stars},{Genre},{Votes},{Gross},{MetaScore},{MPAA},{MovieType},{FetchDate})";

            foreach (var m in movies)
            {
                var id = System.Guid.NewGuid();
                var cmd = query
                    .ReplaceScript("{Id}", id)
                    .ReplaceScript("{Title}", m.Title)
                    .ReplaceScript("{Year}", m.Year)
                    .ReplaceScript("{YearEnd}", m.YearEnd)
                    .ReplaceScript("{Duration}", m.Duration)
                    .ReplaceScript("{Rating}", m.Rating)
                    .ReplaceScript("{Directors}", m.Directors)
                    .ReplaceScript("{Stars}", m.Stars)
                    .ReplaceScript("{Genre}", m.Genre)
                    .ReplaceScript("{Votes}", m.Votes)
                    .ReplaceScript("{Gross}", m.Gross)
                    .ReplaceScript("{ImdbId}", m.ImdbId)
                    .ReplaceScript("{MetaScore}", m.MetaScore)
                    .ReplaceScript("{MPAA}", m.MPAA)
                    .ReplaceScript("{MovieType}", m.MovieType)
                    .ReplaceScript("{FetchDate}", m.FetchDate);

                executeCommand(cmd, true);
            }
        }
        public void SaveMovieFiles(List<MovieFile> movieFiles)
        {
            var query = @"
update MovieFiles set MovieId={MovieId},ProbableImdbIds={ProbableImdbIds},Probability={Probability} where Id={Id}
if(@@ROWCOUNT=0)
insert MovieFiles 
    (Id,FileName,DirName,Size,MovieId,ProbableImdbIds,Probability)
	values		
    ({Id},{FileName},{DirName},{Size},{MovieId},{ProbableImdbIds},{Probability})
";

            foreach (var item in movieFiles)
            {
                var id = item.Id == default(Guid) ? Guid.NewGuid() : item.Id;
                var cmd = query
                    .ReplaceScript("{Id}", item.Id)
                    .ReplaceScript("{FileName}", item.FileName)
                    .ReplaceScript("{DirName}", item.DirName)
                    .ReplaceScript("{Size}", item.Size)
                    .ReplaceScript("{MovieId}", item.MovieId)
                    .ReplaceScript("{ProbableImdbIds}", item.ProbableImdbIds)
                    .ReplaceScript("{Probability}", item.Probability);
                executeCommand(cmd, true);
            }
        }
        public List<MovieBrief> GetMovieBriefList()
        {
            List<MovieBrief> result;
            var query = @"
select 
	m.Id,
	m.Title,
	m.Year,
	m.ImdbId
 from Movies m ";
            result = executeQuery<MovieBrief>(query, false);
            return result;
        }
        public List<MovieFile> GetMovieFileList()
        {
            List<MovieFile> result;
            var query = @"
select 
	m.MovieFileId Id,
	m.DirName,
	m.FileName,
	m.Size,
	m.MovieId,
	m.ImdbId,
	m.Title,
	m.Year,
	m.Probability,
	m.ProbableImdbIds,
    cast (0 as bit) IsMissing,
    cast (0 as bit) IsDuplicate
 from MovieList m where Available = 1";
            result = executeQuery<MovieFile>(query, false);
            return result;
        }
        public List<Movie> GetMovieList()
        {
            List<Movie> result;
            var query = @"
SELECT MovieId
      ,ImdbId
      ,Title
      ,Year
      ,Rating
      ,Directors
      ,Stars
      ,Genre
      ,Votes
      ,Gross
      ,FetchDate
      ,MetaScore
      ,MPAA
      ,MovieFileId
      ,DirName
      ,FileName
      ,Available
      ,NULL Language
  FROM dbo.MovieList
  order by isnull(Votes,1)*isnull(Rating,1)+isnull(Gross,0)/150 desc";
            result = executeQuery<Movie>(query, false);
            return result;
        }

        internal void UpdateSelectedPath(List<SearchPath> paths)
        {
            var command = "DELETE dbo.SearchPaths\n";
            foreach (var item in paths)
            {
                command += "INSERT dbo.SearchPaths(Path,IsActive)VALUES({path},{isActive})\n"
                    .ReplaceScript("{path}",item.Path)
                    .ReplaceScript("{isActive}", item.IsActive);
            }
            executeCommand(command, false);
        }

        internal List<SearchPath> GetSearchPaths()
        {
            var query = "SELECT sp.Path, sp.IsActive FROM dbo.SearchPaths sp";
            var result = executeQuery<SearchPath>(query, false);
            return result;
        }
    }
}

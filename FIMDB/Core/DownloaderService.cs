using Farzin.Infrastructure.CrossCutting.Audit;
using Farzin.Infrastructure.CrossCutting.Factories;
using FIMDB.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FIMDB.Core
{
    class DownloaderService : IDisposable
    {
        //var url = "http://www.imdb.com/search/title?genres=action&title_type=feature&view=advanced&sort=moviemeter,asc&page={page}&ref_=adv_nxt";
        const string searchTVShows_popularity_a = "https://www.imdb.com/search/title?adult=include&num_votes=2000,&title_type=tv_series&user_rating=5.0,&page={page}&sort=moviemeter,asc&ref_=adv_nxt";
        const string searchMovies_alpha_a = "https://www.imdb.com/search/title?adult=include&num_votes=2000,&title_type=feature&user_rating=5.0,&page={page}&sort=alpha,desc&ref_=adv_nxt";
        const string searchMovies_alpha_d = "https://www.imdb.com/search/title?adult=include&num_votes=2000,&title_type=feature&user_rating=5.0,&page={page}&sort=alpha,asc&ref_=adv_nxt";
        const string searchUrl_popularity2 = "https://www.imdb.com/search/title?adult=include&num_votes=2000,&title_type=feature&user_rating=5.0,&page={page}&sort=moviemeter,asc&ref_=adv_nxt";
        const string searchUrl_imdbRating1 = "https://www.imdb.com/search/title?adult=include&num_votes=2000,&title_type=feature&user_rating=5.0,&page={page}&sort=user_rating,desc&ref_=adv_nxt";
        const string searchUrl_imdbRating2 = "https://www.imdb.com/search/title?adult=include&num_votes=2000,&title_type=feature&user_rating=5.0,&page={page}&sort=user_rating,asc&ref_=adv_nxt";
        const string searchUrl_numberOfVotes1 = "https://www.imdb.com/search/title?adult=include&num_votes=2000,&title_type=feature&user_rating=5.0,&page={page}&sort=num_votes,desc&ref_=adv_nxt";
        const string searchUrl_numberOfVotes2 = "https://www.imdb.com/search/title?adult=include&num_votes=2000,&title_type=feature&user_rating=5.0,&page={page}&sort=num_votes,asc&ref_=adv_nxt";
        const string searchUrl_usBox1 = "https://www.imdb.com/search/title?adult=include&num_votes=2000,&title_type=feature&user_rating=5.0,&page={page}&sort=boxoffice_gross_us,desc&ref_=adv_nxt";
        const string searchUrl_usBox2 = "https://www.imdb.com/search/title?adult=include&num_votes=2000,&title_type=feature&user_rating=5.0,&page={page}&sort=boxoffice_gross_us,asc&ref_=adv_nxt";
        const string searchUrl_runtime1 = "https://www.imdb.com/search/title?adult=include&num_votes=2000,&title_type=feature&user_rating=5.0,&page={page}&sort=runtime,desc&ref_=adv_nxt";
        const string searchUrl_runtime2 = "https://www.imdb.com/search/title?adult=include&num_votes=2000,&title_type=feature&user_rating=5.0,&page={page}&sort=runtime,asc&ref_=adv_nxt";
        const string searchUrl_year1 = "https://www.imdb.com/search/title?adult=include&num_votes=2000,&title_type=feature&user_rating=5.0,&page={page}&sort=year,desc&ref_=adv_nxt";
        const string searchUrl_year2 = "https://www.imdb.com/search/title?adult=include&num_votes=2000,&title_type=feature&user_rating=5.0,&page={page}&sort=year,asc&ref_=adv_nxt";
        const string searchUrl_releaseDate1 = "https://www.imdb.com/search/title?adult=include&num_votes=2000,&title_type=feature&user_rating=5.0,&page={page}&sort=release_date,desc&ref_=adv_nxt";
        const string searchUrl_releaseDate2 = "https://www.imdb.com/search/title?adult=include&num_votes=2000,&title_type=feature&user_rating=5.0,&page={page}&sort=release_date,asc&ref_=adv_nxt";
        private HttpClient _httpClient;

        public DownloaderService()
        {
            _httpClient = HttpClientFactory.CreateHttpClient();
        }
        private List<MovieInfo> parseMoviesFromHtml(string html)
        {
            var result = new List<MovieInfo>();
            if (string.IsNullOrEmpty(html))
                return result;
            MovieType movieType = 0;
            var now = DateTime.Now;
            var tPageTitle = "<title>";
            var tRowNO = "<span class=\"lister-item-index unbold text-primary\">";
            var tTitle = "<a href=\"/title/";
            var tYear = "<span class=\"lister-item-year text-muted unbold\">";
            var tMpaa = "<span class=\"certificate";
            var tDuration = "<span class=\"runtime\">";
            var tGenre = "<span class=\"genre\">";
            var tDirectors = "/?ref_=adv_li_dr_";
            var tStars = "/?ref_=adv_li_st_";
            var tVotes1 = "<span class=\"text-muted\">Votes:</span>";
            var tVotes2 = "<span name=\"nv\" data-value=\"";
            var tMeta = "<span class=\"metascore";
            var tRating = "<span class=\"rating-rating \"><span class=\"value\">";
            var tGross = "<span class=\"text-muted\">Gross:</span>";

            string rowNo, imdbId, title, year, directors, stars,
                genre = null, votes = null, rate = null,
                gross = null, metaScore = null, mpaa = null,
                duration = null;

            var i1 = html.IndexOf(tPageTitle) + tPageTitle.Length;
            var pageTitle = html.Substring(i1, html.IndexOf("</", i1)).ToLower();
            if (pageTitle.IndexOf("feature films") != -1)
                movieType = MovieType.Cinema;
            else if (pageTitle.IndexOf("tv series") != -1)
                movieType = MovieType.TVSeries;

            i1 = html.IndexOf(tRowNO);
            while (true)
            {
                //RowNO
                i1 += tRowNO.Length;
                var i2 = html.IndexOf(".", i1);
                rowNo = html.Substring(i1, i2 - i1);

                var imax = html.IndexOf(tRowNO, i1);
                if (imax == -1)
                    imax = html.Length;

                //Id
                var i3 = html.IndexOf(tTitle, i2);
                i3 += tTitle.Length;
                var i4 = html.IndexOf("/", i3);
                imdbId = html.Substring(i3, i4 - i3);

                //Title
                var iTitle1 = html.IndexOf(">", i4);
                iTitle1++;
                var iTitle2 = html.IndexOf("</", iTitle1);
                title = html.Substring(iTitle1, iTitle2 - iTitle1);

                //Year
                int iYear1, iYear2;
                iYear1 = html.IndexOf(tYear, iTitle2, imax - iTitle2);
                iYear1 += tYear.Length;
                iYear2 = html.IndexOf("</", iYear1);
                year = html.Substring(iYear1, iYear2 - iYear1);

                //Mpaa
                int iMpaa1, iMpaa2 = iYear2;
                iMpaa1 = html.IndexOf(tMpaa, iMpaa2, imax - iMpaa2);
                if (iMpaa1 != -1)
                {
                    iMpaa1 += tMpaa.Length;
                    iMpaa1 = html.IndexOf(">", iMpaa1) + 1;
                    iMpaa2 = html.IndexOf("<", iMpaa1);
                    mpaa = html.Substring(iMpaa1, iMpaa2 - iMpaa1).Trim();
                }

                //Duration
                int iDuration1, iDuration2 = iMpaa2;
                iDuration1 = html.IndexOf(tDuration, iDuration2, imax - iDuration2);
                if (iDuration1 != -1)
                {
                    iDuration1 += tDuration.Length;
                    iDuration1 = html.IndexOf(">", iDuration1) + 1;
                    iDuration2 = html.IndexOf("<", iDuration1);
                    duration = html.Substring(iDuration1, iDuration2 - iDuration1).Trim();
                }

                //genre
                int iGenre1, iGenre2 = iDuration2;
                iGenre1 = html.IndexOf(tGenre, iYear2, imax - iYear2);
                if (iGenre1 != -1)
                {
                    iGenre1 += tGenre.Length;
                    iGenre2 = html.IndexOf("</", iGenre1);
                    genre = html.Substring(iGenre1, iGenre2 - iGenre1).Trim();
                }

                //rate
                int iRate1, iRate2 = iGenre2;
                iRate1 = html.IndexOf(tRating, iGenre2, imax - iGenre2);
                if (iRate1 != -1)
                {
                    iRate1 += tRating.Length;
                    iRate2 = html.IndexOf("</", iRate1);
                    rate = html.Substring(iRate1, iRate2 - iRate1);
                }

                //meta score
                int iMeta1, iMeta2 = iGenre2;
                iMeta1 = html.IndexOf(tMeta, iMeta2, imax - iMeta2);
                if (iMeta1 != -1)
                {
                    iMeta1 += tMeta.Length;
                    iMeta1 = html.IndexOf(">", iMeta1) + 1;
                    iMeta2 = html.IndexOf("<", iMeta1);
                    metaScore = html.Substring(iMeta1, iMeta2 - iMeta1).Trim();
                }

                //directors
                int iDirectors1, iDirectors2 = iMeta2;
                directors = "";
                while (true)
                {
                    iDirectors1 = html.IndexOf(tDirectors, iDirectors2, imax - iDirectors2);
                    if (iDirectors1 == -1)
                        break;
                    iDirectors1 += tDirectors.Length;
                    iDirectors1 = html.IndexOf(">", iDirectors1) + 1;
                    iDirectors2 = html.IndexOf("</", iDirectors1);
                    directors += html.Substring(iDirectors1, iDirectors2 - iDirectors1) + ",";
                }
                directors = directors.TrimEnd(',');

                //stars
                int iStars1, iStars2 = iDirectors2;
                stars = "";
                while (true)
                {
                    iStars1 = html.IndexOf(tStars, iStars2, imax - iStars2);
                    if (iStars1 == -1)
                        break;
                    iStars1 += tDirectors.Length;
                    iStars1 = html.IndexOf(">", iStars1) + 1;
                    iStars2 = html.IndexOf("</", iStars1);
                    stars += html.Substring(iStars1, iStars2 - iStars1) + ",";
                }
                stars = stars.TrimEnd(',');

                //votes
                int iVotes1, iVotes2 = iStars2;
                iVotes1 = html.IndexOf(tVotes1, iVotes2, imax - iVotes2);
                if (iVotes1 != -1)
                {
                    iVotes1 += tVotes1.Length;
                    iVotes1 = html.IndexOf(tVotes2, iVotes1);
                    iVotes1 += tVotes2.Length;
                    iVotes2 = html.IndexOf("\"", iVotes1);
                    votes = html.Substring(iVotes1, iVotes2 - iVotes1);
                }

                //gross
                int iGross1, iGross2 = iVotes2;
                iGross1 = html.IndexOf(tGross, iGross2, imax - iGross2);
                if (iGross1 != -1)
                {
                    iGross1 += tVotes1.Length;
                    iGross1 = html.IndexOf(tVotes2, iGross1);
                    iGross1 += tVotes2.Length;
                    iGross2 = html.IndexOf("\"", iGross1);
                    gross = html.Substring(iGross1, iGross2 - iGross1);
                }

                result.Add(new MovieInfo
                {
                    Directors = directors,
                    Genre = genre,
                    ImdbId = imdbId,
                    Rating = getRate(rate),
                    Stars = stars,
                    Title = title,
                    Votes = getVotes(votes),
                    Year = getYear(year),
                    YearEnd = getEndYear(year),
                    Gross = getGross(gross),
                    FetchDate = now,
                    MetaScore = getMetaScore(metaScore),
                    MPAA = mpaa,
                    MovieType = movieType,
                    Duration = getDuration(duration),
                });

                if (imax == html.Length)
                    break;
                i1 = imax;
            }

            return result;
        }

        private int? getDuration(string duration)
        {
            if (string.IsNullOrEmpty(duration))
                return null;
            var result = int.Parse(duration.Replace("min", "").Trim());
            return result;
        }

        private int? getMetaScore(string v)
        {
            if (string.IsNullOrEmpty(v))
                return null;
            var result = int.Parse(v);
            return result;
        }
        private decimal? getGross(string v)
        {
            if (string.IsNullOrEmpty(v))
                return null;
            var result = decimal.Parse(v);
            return result;
        }
        private int? getVotes(string v)
        {
            if (string.IsNullOrEmpty(v))
                return null;
            var result = int.Parse(v);
            return result;
        }
        private float? getRate(string v)
        {
            if (string.IsNullOrEmpty(v))
                return null;
            var result = float.Parse(v);
            return result;
        }
        private int? getEndYear(string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;
            var i = str.LastIndexOf('(') + 1;
            var j = str.LastIndexOf(')');
            if (j == -1)
                j = str.Length;
            var y = str.Substring(i, j - i).Trim();
            
            i = y.IndexOf('–');
            if (i == -1)
                return null;

            y = y.Substring(i + 1);

            i = y.IndexOf(' ');
            if (i != -1)
                y = y.Remove(i);

            if (string.IsNullOrEmpty(y))
                return null;

            int result = int.Parse(y);
            return result;
        }
        private int? getYear(string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;
            var i = str.LastIndexOf('(') + 1;
            var j = str.LastIndexOf(')');
            if (j == -1)
                j = str.Length;
            var y = str.Substring(i, j - i).Trim();

            i = y.IndexOf('–');
            if (i != -1)
                y = y.Remove(i);

            i = y.IndexOf(' ');
            if (i != -1)
                y = y.Remove(i);

            int result = int.Parse(y);
            return result;
        }
        public async Task<List<MovieInfo>> GetMoviesInPage_MultiUrl(int page)
        {
            var result = new List<MovieInfo>();
            var tasks = new List<Task<string>>();
            tasks.Add(_httpClient.GetStringAsync(searchTVShows_popularity_a.Replace("{page}", page.ToString())));
            tasks.Add(_httpClient.GetStringAsync(searchMovies_alpha_a.Replace("{page}", page.ToString())));
            tasks.Add(_httpClient.GetStringAsync(searchMovies_alpha_d.Replace("{page}", page.ToString())));
            var htmls = await Task.WhenAll(tasks);
            foreach (var html in htmls)
            {
                try
                {
                    var partialResult = parseMoviesFromHtml(html);
                    foreach (var pr in partialResult)
                    {
                        if (result.Any(a => a.ImdbId == pr.ImdbId) == false)
                        {
                            result.Add(pr);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                }
            }
            return result;
        }
        //public async Task<List<MovieInfo>> GetMoviesInPage(int page)
        //{
        //    var result = new List<MovieInfo>();
        //    var html = await _httpClient.GetStringAsync(searchUrl_popularity1.Replace("{page}", page.ToString()));
        //    try
        //    {
        //        var partialResult = parseMoviesFromHtml(html);
        //        foreach (var pr in partialResult)
        //        {
        //            if (result.Any(a => a.ImdbId == pr.ImdbId) == false)
        //            {
        //                result.Add(pr);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log(ex);
        //    }
        //    return result;
        //}
        public List<MovieInfo> LoadPreviouslySavedMoviesFromTextFile(string file)
        {
            var lines = File.ReadLines(file);
            var result = new List<MovieInfo>();
            var now = DateTime.Now;
            foreach (var l in lines)
            {
                var arrayInfo = l.Split('|');

                result.Add(new MovieInfo
                {
                    ImdbId = arrayInfo[0],
                    Title = arrayInfo[1],
                    Year = getYear(arrayInfo[2]),
                    Rating = getRate(arrayInfo[3]),
                    Directors = arrayInfo[4],
                    Stars = arrayInfo[5],
                    Genre = arrayInfo[6],
                    Votes = getVotes(arrayInfo[7]),
                    Gross = getGross(arrayInfo[8]),
                    FetchDate = now,
                });
            }
            return result;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}

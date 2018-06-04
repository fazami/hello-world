using System;

namespace FIMDB.Model
{
    class MovieInfo
    {
        public string Title { get; set; }
        public float? Rating { get; set; }
        public int? Year { get; set; }
        public string Stars { get; set; }
        public string Directors { get; set; }
        public string Genre { get; set; }
        public string ImdbId { get; set; }
        public int? Votes { get; set; }
        public decimal? Gross { get; set; }
        public string MPAA { get; set; }
        public int? MetaScore { get; set; }
        public string Language { get; set; }
        public DateTime FetchDate { get; set; }
        public MovieType MovieType { get; set; }
        public int? Duration { get; set; }
        public int? YearEnd { get; set; }
    }

}

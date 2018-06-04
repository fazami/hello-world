using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;

namespace FIMDB.Model
{
    [Table("Movies")]
    public class Movie
    {
        public Movie()
        {
            Genres = new List<string>();
            Actors = new List<string>();
            Directors = new List<string>();
            Writers = new List<string>();
            Languages = new List<string>();
            Countries = new List<string>();
            Awards = new List<string>();
        }
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string ImdbId { get; set; }
        public float ImdbRating { get; set; }
        public int ImdbTotalVotes { get; set; }
        public string Rated { get; set; }
        public int MetaScore { get; set; }
        [NotMapped]
        public string PosterAddressOnWeb { get; set; }
        [NotMapped]
        public List<string> Genres { get; set; }
        [NotMapped]
        public List<string> Actors { get; set; }
        [NotMapped]
        public List<string> Directors { get; set; }
        [NotMapped]
        public List<string> Writers { get; set; }
        [NotMapped]
        public List<string> Languages { get; set; }
        public string FullFileName { get; set; }
        [NotMapped]
        public List<string> Awards { get; set; }
        [NotMapped]
        public List<string> Countries { get; set; }
        [NotMapped]
        public long FileSize { get; set; }
        [NotMapped]
        public byte[] Poster { get; set; }
        [NotMapped]
        public string FileName
        {
            get { return Path.GetFileName(FullFileName); }
        }
        [Column("Genres")]
        public string GenresFormatted
        {
            get { return string.Join(", ", Genres); }
            set { Genres = string.IsNullOrEmpty(value) ? new List<string>() : value.Split(',').Select(a => a.Trim()).ToList(); }
        }
        [Column("Actors")]
        public string ActorsFormatted
        {
            get { return string.Join(", ", Actors); }
            set { Actors = string.IsNullOrEmpty(value) ? new List<string>() : value.Split(',').Select(a => a.Trim()).ToList(); }
        }
        [Column("Directors")]
        public string DirectorsFormatted
        {
            get { return string.Join(", ", Directors); }
            set { Directors = string.IsNullOrEmpty(value) ? new List<string>() : value.Split(',').Select(a => a.Trim()).ToList(); }
        }
        [Column("Writers")]
        public string WritersFormatted
        {
            get { return string.Join(", ", Writers); }
            set { Writers = string.IsNullOrEmpty(value) ? new List<string>() : value.Split(',').Select(a => a.Trim()).ToList(); }
        }
        [Column("Languages")]
        public string LanguagesFormatted
        {
            get { return string.Join(", ", Languages); }
            set { Languages = string.IsNullOrEmpty(value) ? new List<string>() : value.Split(',').Select(a => a.Trim()).ToList(); }
        }
        [Column("Countries")]
        public string CountriesFormatted
        {
            get { return string.Join(", ", Countries); }
            set { Countries = string.IsNullOrEmpty(value) ? new List<string>() : value.Split(',').Select(a => a.Trim()).ToList(); }
        }
        [Column("Awards")]
        public string AwardsFormatted
        {
            get { return string.Join(", ", Awards); }
            set { Awards= string.IsNullOrEmpty(value) ? new List<string>() : value.Split(',').Select(a => a.Trim()).ToList(); }
        }
        [NotMapped]
        public float FileSizeMB
        {
            get { return (float)(FileSize / 1024m / 1024); }
        }

        public float? Gross { get; internal set; }
    }
}

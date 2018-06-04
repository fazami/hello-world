using System;

namespace FIMDB.Model
{
    class MovieBrief
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int? Year { get; set; }
        public string ImdbId { get; set; }
    }
}

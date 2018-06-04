using System;

namespace FIMDB.Model
{
    class MovieFile
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string DirName { get; set; }
        public long Size { get; set; }
        public Guid? MovieId { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public int? Year { get; set; }
        public int Probability { get; set; }
        public string ProbableImdbIds { get; set; }
        public bool IsMissing { get; set; }
        public bool IsDuplicate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FIMDB.DB.Files
{
    public class MovieFileModel
    {
        public string FileName { get; set; }
        public long Size { get; set; }
        public byte[] Poster { get; set; }
    }
}

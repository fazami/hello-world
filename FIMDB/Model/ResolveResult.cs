using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMDB.Model
{
    class ResolveResult
    {
        public bool Success { get { return Probability > 40; } }
        public int Probability { get; set; }
        public List<MovieBrief> Movies { get; set; }
    }
}

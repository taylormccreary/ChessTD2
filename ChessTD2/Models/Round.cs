using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessTD2.Models
{
    public class Round
    {
        public int RoundID { get; set; }
        public Section Section { get; set; }
        public byte Number { get; set; }

        public virtual ICollection<Pairing> Pairings { get; set; }
    }
}
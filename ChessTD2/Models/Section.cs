using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessTD2.Models
{
    public class Section
    {
        public int SectionID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Round> Rounds { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        //public virtual ICollection<Round> Rounds { get; set; }
        public Tournament Tournament { get; set; }
    }
}
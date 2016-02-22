using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessTD2.Models
{
    public class Tournament
    {
        public int TournamentID { get; set; }
        public string Name { get; set; }

        //public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
    }
}
using System.Collections.Generic;

namespace ChessTD2.Models
{
    public class Player
    {
        public int PlayerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Rating { get; set; }

        //public virtual ICollection<Tournament> Tournaments { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
    }
}
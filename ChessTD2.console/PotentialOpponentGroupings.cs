using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTD2.console
{
    class PotentialOpponentGroupings
    {
        public int PlayerID { get; set; }
        public int Rating { get; set; }

        public double Score { get; set; }
        public List<int> OpponentPlayerIDs { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTD2.console
{
    public class PotentialOpponentGroupings
    {
        public int PlayerID { get; set; }
        public int Rating { get; set; }

        public double Score { get; set; }
        public List<int> OpponentPlayerIDs { get; set; }

        public int RelativeScoreGroup { get; set; }
        public int OpponentGroup { get; set; }
        public int SameScoreGroupHalf { get; set; } = 999;
    }
}

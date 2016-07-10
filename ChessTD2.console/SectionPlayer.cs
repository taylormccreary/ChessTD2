using System.Collections.Generic;


namespace ChessTD2.console
{
    public class SectionPlayer
    {

        public int PlayerID { get; set; }
        public int Rating { get; set; }

        public List<PairingResult> RoundResults { get; set; }
        public List<int> OpponentPlayerIDs { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTD2.console
{
    public class Pairing
    {
        public int WhitePlayerID { get; set; }
        public int BlackPlayerID { get; set; }

        public PairingResult Result { get; set; }
        public int RoundNumber { get; set; }
    }
    
    public enum PairingResult { WhiteWins, BlackWins, Draw, Unknown }
}

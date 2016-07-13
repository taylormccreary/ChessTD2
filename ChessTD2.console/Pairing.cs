using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTD2.console
{
    public class Pairing
    {
        public SectionPlayer White { get; set; }
        public SectionPlayer Black { get; set; }

        public PairingResult Result { get; set; }
        public int RoundNumber { get; set; }
    }
    
    public enum PairingResult { WhiteWins, BlackWins, Draw, Unknown }
}

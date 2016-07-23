using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTD2.console
{
    class PairingLogic
    {
        public List<PairingLogicPlayer> Players { get; set; }
    }

    class PairingLogicPlayer
    {
        public SectionPlayer Player { get; set; }
        public List<SectionPlayer> PreferenceList { get; set; }
        public int CurrentProposal { get; set; }
    }
}

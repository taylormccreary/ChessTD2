using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTD2.console
{
    public class PreferenceList
    {
        public int PlayerID { get; set; }
        public List<int> PreferenceListIDs { get; set; }
        public int CurrentProposal { get; set; }
    }
}

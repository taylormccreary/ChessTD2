using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTD2.console
{
    public class Standings
    {

        public List<SectionPlayer> SectionPlayers { get; set; }

        public void addRoundResults(List<Pairing> round)
        {
            foreach (var pairing in round)
            {
                var white = SectionPlayers.Where(p => p.PlayerID == pairing.WhitePlayerID).First();
                var black = SectionPlayers.Where(p => p.PlayerID == pairing.BlackPlayerID).First();

                white.OpponentPlayerIDs.Add(pairing.BlackPlayerID);
                black.OpponentPlayerIDs.Add(pairing.WhitePlayerID);

                if (pairing.Result == PairingResult.WhiteWins)
                {
                    white.RoundResults.Add(1);
                    black.RoundResults.Add(0);
                }else if (pairing.Result == PairingResult.BlackWins)
                {
                    white.RoundResults.Add(0);
                    black.RoundResults.Add(1);
                }else if (pairing.Result == PairingResult.Draw)
                {
                    white.RoundResults.Add(.5);
                    black.RoundResults.Add(.5);
                }
            }
        }
    }
}

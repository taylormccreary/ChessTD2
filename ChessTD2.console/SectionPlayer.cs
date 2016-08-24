using System.Collections.Generic;
using System.Linq;


namespace ChessTD2.console
{
    public class SectionPlayer
    {

        public int PlayerID { get; set; }
        public int Rating { get; set; }

        public List<double> RoundResults { get; set; }

        public List<int> RoundColors { get; set; }
        public List<int> OpponentPlayerIDs { get; set; }

        public ColorStatus CalculateColorStatus()
        {
            
            if (RoundColors.Count() == 0)
            {
                return ColorStatus.None;
            }
            else if (RoundColors.Sum() > 0)
            {
                return ColorStatus.NeedsBlack;
            }
            else if (RoundColors.Sum() < 0)
            {
                return ColorStatus.NeedsWhite;
            }
            else if (RoundColors.Sum() == 0 && RoundColors.Last() == 1)
            {
                return ColorStatus.DueBlack;
            }
            else
            {
                return ColorStatus.DueWhite;
            }

        }

    }
}

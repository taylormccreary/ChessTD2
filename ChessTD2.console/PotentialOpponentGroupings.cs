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
        public int RelativeColorGroup { get; set; }

        public static int CalculateRelativeColorGroup(ColorStatus current, ColorStatus potentialOpponent)
        {
            if (current == ColorStatus.DueBlack || current == ColorStatus.NeedsBlack)
            {
                switch (potentialOpponent)
                {
                    case ColorStatus.NeedsWhite:
                        return 1;
                    case ColorStatus.DueWhite:
                        return 2;
                    case ColorStatus.None:
                        return 3;
                    case ColorStatus.DueBlack:
                        return 4;
                    case ColorStatus.NeedsBlack:
                        return 4;
                    default:
                        break;
                }
            }
            else if (current == ColorStatus.DueWhite || current == ColorStatus.NeedsWhite)
            {
                switch (potentialOpponent)
                {
                    case ColorStatus.NeedsBlack:
                        return 1;
                    case ColorStatus.DueBlack:
                        return 2;
                    case ColorStatus.None:
                        return 3;
                    case ColorStatus.DueWhite:
                        return 4;
                    case ColorStatus.NeedsWhite:
                        return 4;
                    default:
                        break;
                }
            }
            return 4;
        }
    }
}

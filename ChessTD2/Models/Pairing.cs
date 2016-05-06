namespace ChessTD2.Models
{
    public enum PairingResult { WhiteWins, BlackWins, Draw, NoResult}
    public class Pairing
    {
        public int PairingID { get; set; }
        public Player White { get; set; }
        public Player Black { get; set; }
        public Round Round { get; set; }
        public PairingResult Result { get; set; }
    }
}
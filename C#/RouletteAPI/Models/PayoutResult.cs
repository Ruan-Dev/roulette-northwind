namespace Roulette.API.Models
{
    public class PayoutResult
    {
        public int ProcessedBetCount { get; set; }
        public List<Bet> WinningBets { get; set; }
    }
}

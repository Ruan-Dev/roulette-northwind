namespace Roulette.API.Models
{
    public class BetRequest
    {
        public int UserId { get; set; }
        public string? BetType { get; set; }
        public List<int>? Number { get; set; }
        public float Amount { get; set; }
    }

}

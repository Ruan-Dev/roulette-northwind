using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Roulette.API.Models
{
    public class Bet
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public BetTypeConstants.BetType BetType { get; set; }
        public bool IsProcessed { get; set; }
        [NotMapped]
        public List<int>? Number
        {
            get => NumberJson == null ? null : JsonSerializer.Deserialize<List<int>>(NumberJson);
            set => NumberJson = value == null ? "[]" : JsonSerializer.Serialize(value);
        }

        [Column("Number")]
        public string NumberJson { get; set; } = "[]";

        public float Amount { get; set; }
        public float PayoutAmount { get; set; }
    }

}

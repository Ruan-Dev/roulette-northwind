namespace Roulette.API.Models
{
    public static class BetTypeConstants
    {
        public enum BetType
        {
            Single,
            Split,
            Street,
            Corner,
            SixLine,
            Red,
            Black,
            Even,
            Odd,
            OneToEighteen,
            NineteenToThirtySix,
            FirstDozen,
            SecondDozen,
            ThirdDozen,
            ColumnOne,
            ColumnTwo,
            ColumnThree
        }

        public static readonly List<BetType> NumberBets = new List<BetType> {
            BetType.Single,
            BetType.Split,
            BetType.Street,
            BetType.Corner,
            BetType.SixLine
        };

        public static readonly List<BetType> EvenBets = new List<BetType> {
            BetType.Even,
            BetType.Odd,
            BetType.Black,
            BetType.Red,
            BetType.OneToEighteen,
            BetType.NineteenToThirtySix
        };

        public static readonly List<BetType> DozenAndColumnBets = new List<BetType> {
            BetType.FirstDozen,
            BetType.SecondDozen,
            BetType.ThirdDozen,
            BetType.ColumnOne,
            BetType.ColumnTwo,
            BetType.ColumnThree
        };
    }
}

using Roulette.API.Interfaces;
using Roulette.API.Models;

namespace Roulette.API.Services
{
    public class PayoutService : IPayoutService
    {
        public PayoutResult CalculatePayout(SpinResult spinResult, List<Bet> bets)
        {
            List<Bet> winningBets = new List<Bet>();
            int processedBetCount = 0;
            foreach (var bet in bets)
            {
                if (IsWinningBet(bet, spinResult))
                {
                    // Calculate the winnings based on bet type and amount
                    float winAmount = CalculateWinnings(bet);
                    bet.PayoutAmount = bet.Amount + winAmount;
                    winningBets.Add(bet);
                }

                processedBetCount++;
            }

            PayoutResult result = new PayoutResult()
            {
                ProcessedBetCount = processedBetCount,
                WinningBets = winningBets
            };
            return result;
        }

        private static bool IsWinningBet(Bet bet, SpinResult spinResult)
        {
            //Check bet types that explicitly contain numbers
            if (bet.BetType.Equals(BetTypeConstants.BetType.Single) || bet.BetType.Equals(BetTypeConstants.BetType.Split) || bet.BetType.Equals(BetTypeConstants.BetType.Street) || bet.BetType.Equals(BetTypeConstants.BetType.Corner) || bet.BetType.Equals(BetTypeConstants.BetType.SixLine))
            {
                if (bet.Number == null)
                {
                    throw new InvalidOperationException("Invalid bet.");
                }
                if (bet.Number.Contains(spinResult.SpinNumber))
                {
                    return true;
                }
            }

            //Check even bets
            if (bet.BetType.Equals(BetTypeConstants.BetType.Even))
            {
                if ((spinResult.SpinNumber % 2) == 0)
                {
                    return true;
                }
            }

            //Check odd bets
            if (bet.BetType.Equals(BetTypeConstants.BetType.Odd))
            {
                if ((spinResult.SpinNumber % 2) != 0)
                {
                    return true;
                }
            }

            //Check black/red bets
            if (bet.BetType.Equals(BetTypeConstants.BetType.Black) || bet.BetType.Equals(BetTypeConstants.BetType.Red))
            {
                if (spinResult.ResultColour.Equals(bet.BetType))
                {
                    return true;
                }
            }

            //Check low bets
            if (bet.BetType.Equals(BetTypeConstants.BetType.OneToEighteen))
            {
                if (0 <= spinResult.SpinNumber && spinResult.SpinNumber <= 18)
                {
                    return true;
                }
            }

            //Check high bets
            if (bet.BetType.Equals(BetTypeConstants.BetType.NineteenToThirtySix))
            {
                if (19 <= spinResult.SpinNumber && spinResult.SpinNumber <= 36)
                {
                    return true;
                }
            }

            //Check first dozen
            if (bet.BetType.Equals(BetTypeConstants.BetType.FirstDozen))
            {
                if (1 <= spinResult.SpinNumber && spinResult.SpinNumber <= 12)
                {
                    return true;
                }
            }

            //Check second dozen
            if (bet.BetType.Equals(BetTypeConstants.BetType.SecondDozen))
            {
                if (13 <= spinResult.SpinNumber && spinResult.SpinNumber <= 24)
                {
                    return true;
                }
            }

            //Check third dozen
            if (bet.BetType.Equals(BetTypeConstants.BetType.ThirdDozen))
            {
                if (25 <= spinResult.SpinNumber && spinResult.SpinNumber <= 36)
                {
                    return true;
                }
            }

            //Check column 1
            if (bet.BetType.Equals(BetTypeConstants.BetType.ColumnOne))
            {
                int[] columnNumber = new int[] { 1, 4, 7, 10, 13, 16, 19, 22, 25, 28, 31, 34 };
                if (columnNumber.Contains(spinResult.SpinNumber))
                {
                    return true;
                }
            }

            //Check column 2
            if (bet.BetType.Equals(BetTypeConstants.BetType.ColumnTwo))
            {
                int[] columnNumber = new int[] { 2, 5, 8, 11, 14, 17, 20, 23, 26, 29, 32, 35 };
                if (columnNumber.Contains(spinResult.SpinNumber))
                {
                    return true;
                }
            }

            //Check column 3
            if (bet.BetType.Equals(BetTypeConstants.BetType.ColumnThree))
            {
                int[] columnNumber = new int[] { 3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36 };
                if (columnNumber.Contains(spinResult.SpinNumber))
                {
                    return true;
                }
            }

            return false;
        }

        private static float CalculateWinnings(Bet bet)
        {
            //35:1 payout ratio
            if (bet.BetType.Equals(BetTypeConstants.BetType.Single))
            {
                var winAmount = bet.Amount * 35;
                return winAmount;
            }

            //17:1 payout ratio
            if (bet.BetType.Equals(BetTypeConstants.BetType.Split))
            {
                var winAmount = bet.Amount * 17;
                return winAmount;
            }

            //11:1 payout ratio
            if (bet.BetType.Equals(BetTypeConstants.BetType.Street))
            {
                var winAmount = bet.Amount * 11;
                return winAmount;
            }

            //8:1 payout
            if (bet.BetType.Equals(BetTypeConstants.BetType.Corner))
            {
                var winAmount = bet.Amount * 8;
                return winAmount;
            }

            //5:1 payout ratio
            if (bet.BetType.Equals(BetTypeConstants.BetType.SixLine))
            {
                var winAmount = bet.Amount * 5;
                return winAmount;
            }

            //2:1 payout ratio
            if (BetTypeConstants.DozenAndColumnBets.Contains(bet.BetType))
            {
                var winAmount = bet.Amount * 2;
                return winAmount;
            }

            //1:1 payout ratio
            if (BetTypeConstants.EvenBets.Contains(bet.BetType))
            {
                var winAmount = bet.Amount * 1;
                return winAmount;
            }

            return 0;
        }
    }
}

using Roulette.API.Models;

namespace Roulette.API.Interfaces
{
    public interface IPayoutService
    {
        PayoutResult CalculatePayout(SpinResult spinResult, List<Bet> bets);
    }
}

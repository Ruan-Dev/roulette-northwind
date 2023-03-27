using Roulette.API.Models;

namespace Roulette.API.Interfaces
{
    public interface IRouletteService
    {
        Task<int> PlaceBetAsync(BetRequest bet);
        Task<SpinResult> SpinAsync();
        Task<PayoutResult> PayoutAsync(int spinNumber);
        Task<List<SpinResult>> ShowPreviousSpinsAsync();
    }
}

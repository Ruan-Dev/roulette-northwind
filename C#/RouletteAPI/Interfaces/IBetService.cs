using Roulette.API.Models;

namespace Roulette.API.Interfaces
{
    public interface IBetService
    {
        Task<int> PlaceBetAsync(BetRequest betRequest);
        Task<List<Bet>> GetUnprocessedBetsAsync();
        Task UpdateBetsProcessedAsync(List<Bet> bets);

    }
}

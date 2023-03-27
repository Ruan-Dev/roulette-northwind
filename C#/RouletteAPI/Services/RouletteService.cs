using Roulette.API.Interfaces;
using Roulette.API.Models;

namespace Roulette.API.Services
{
    public class RouletteService : IRouletteService
    {
        private readonly IBetService _betService;
        private readonly ISpinService _spinService;
        private readonly IPayoutService _payoutService;

        public RouletteService(IBetService betService, ISpinService spinService, IPayoutService payoutService)
        {
            _betService = betService;
            _spinService = spinService;
            _payoutService = payoutService;
        }

        public async Task<int> PlaceBetAsync(BetRequest betRequest)
        {
            return await _betService.PlaceBetAsync(betRequest);
        }

        public async Task<SpinResult> SpinAsync()
        {
            return await _spinService.SpinAsync();
        }

        public async Task<PayoutResult> PayoutAsync(int spinNumber)
        {
            var spinResult = (await _spinService.GetSpinResultsAsync()).FirstOrDefault(sr => sr.Id == spinNumber);
            if (spinResult == null)
            {
                return new PayoutResult(); //Returning empty results for simplicity sake
            }

            var unprocessedBets = await _betService.GetUnprocessedBetsAsync();
            var payoutResult = _payoutService.CalculatePayout(spinResult, unprocessedBets);
            await _betService.UpdateBetsProcessedAsync(payoutResult.WinningBets);
            return payoutResult;
        }

        public async Task<List<SpinResult>> ShowPreviousSpinsAsync()
        {
            return await _spinService.GetSpinResultsAsync();
        }
    }
}

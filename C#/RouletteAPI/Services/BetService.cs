using Microsoft.EntityFrameworkCore;
using Roulette.API.Data;
using Roulette.API.Interfaces;
using Roulette.API.Models;

namespace Roulette.API.Services
{
    public class BetService : IBetService
    {
        private readonly IDbContext _context;

        public BetService(IDbContext context)
        {
            _context = context;
        }

        public async Task<int> PlaceBetAsync(BetRequest betRequest)
        {
            Bet betEntry = CreateBetEntry(betRequest);
            _context.Bets.Add(betEntry);
            await _context.SaveChangesAsync();
            return betEntry.Id;
        }

        public async Task<List<Bet>> GetUnprocessedBetsAsync()
        {
            return await _context.Bets
                .Where(b => !b.IsProcessed)
                .ToListAsync();
        }

        public async Task UpdateBetsProcessedAsync(List<Bet> bets)
        {
            foreach (var bet in bets)
            {
                bet.IsProcessed = true;
            }
            await _context.SaveChangesAsync();
        }

        private static Bet CreateBetEntry(BetRequest betRequest)
        {

            if (betRequest.UserId == 0)
            {
                throw new InvalidOperationException("Invalid betRequest. UserId can not be 0");
            }

            BetTypeConstants.BetType betType;
            if (!Enum.TryParse(betRequest.BetType, true, out betType))
            {
                throw new InvalidOperationException(@"Invalid betRequest. " + betRequest.BetType + " not a valid bet type");
            }

            if (betRequest.Amount == 0)
            {
                throw new InvalidOperationException(@"Invalid betRequest. Bet amount can not be 0");
            }

            Bet betEntry = new Bet()
            {
                BetType = betType,
                Amount = betRequest.Amount,
                UserId = betRequest.UserId,
                IsProcessed = false,
                Number = betRequest.Number,
            };

            return betEntry;
        }


       
    }
}

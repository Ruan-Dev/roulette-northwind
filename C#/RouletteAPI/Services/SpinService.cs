using Microsoft.EntityFrameworkCore;
using Roulette.API.Data;
using Roulette.API.Interfaces;
using Roulette.API.Models;

namespace Roulette.API.Services
{
    public class SpinService : ISpinService
    {
        private readonly IDbContext _context;
        private readonly Random _random;

        public SpinService(IDbContext context)
        {
            _context = context;
            _random = new Random();
        }

        public async Task<SpinResult> SpinAsync()
        {
            int spinNumber = _random.Next(0, 37); // 0 to 36 (including 0)
            NumberColour resultColour = DetermineSpinColour(spinNumber);


            SpinResult spinResult = new SpinResult
            {
                SpinNumber = spinNumber,
                ResultColour = resultColour
            };

            _context.SpinResults.Add(spinResult);
            await _context.SaveChangesAsync();
            return spinResult;
        }

        public async Task<List<SpinResult>> GetSpinResultsAsync()
        {
            return await _context.SpinResults.OrderByDescending(s => s.Id).ToListAsync();
        }

        private static NumberColour DetermineSpinColour(int spinNumber)
        {
            if (spinNumber == 0)
            {
                //Result is 0 = green
                return NumberColour.Green;
            }
            else if ((spinNumber % 2) != 0)
            {
                //Result is negative number = black
                return NumberColour.Black;
            }
            else
            {
                //Result is positive number = red
                return NumberColour.Red;
            }
        }


    }
}

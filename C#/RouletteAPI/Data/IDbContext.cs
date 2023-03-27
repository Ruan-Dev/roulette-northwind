using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Roulette.API.Models;

namespace Roulette.API.Data
{
    public interface IDbContext
    {
        DbSet<Bet> Bets { get; set; }
        DbSet<SpinResult> SpinResults { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DatabaseFacade Database { get; }
    }
}

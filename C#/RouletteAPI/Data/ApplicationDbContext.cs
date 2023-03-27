using Microsoft.EntityFrameworkCore;
using Roulette.API.Models;

namespace Roulette.API.Data
{
    public class ApplicationDbContext :DbContext, IDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<SpinResult> SpinResults { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Bet>().ToTable("Bets");
        //    modelBuilder.Entity<SpinResult>().ToTable("SpinResults");
        //}

    }
}

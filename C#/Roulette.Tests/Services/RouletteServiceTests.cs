using Xunit;
using Roulette.API.Services;
using Roulette.API.Models;
using Roulette.API.Data;
using Microsoft.EntityFrameworkCore;
using Roulette.API.Interfaces;

namespace Roulette.Tests.Services
{
    public class RouletteServiceTests
    {
        private readonly IDbContext _dbContext;
        private readonly IRouletteService _rouletteService;

        public RouletteServiceTests()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            var dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureCreated();


            _dbContext = dbContext;

            var betService = new BetService(_dbContext);
            var spinService = new SpinService(_dbContext);
            var payoutService = new PayoutService();

            _rouletteService = new RouletteService(betService, spinService, payoutService);
        }

        [Fact]
        public async Task PlaceBetAsync_ShouldAddBetToDatabase()
        {
            // Arrange
            var bet = new BetRequest { UserId = 1, BetType = "Odd", Amount = 100 };

            // Act
            var betId = await _rouletteService.PlaceBetAsync(bet);

            // Assert
            Assert.True(betId > 0);
            Assert.Single(_dbContext.Bets);
        }

        [Fact]
        public async Task PlaceBetAsync_ValidBet_ReturnsBetId()
        {
            // Arrange
            var bet = new BetRequest
            {
                BetType = "Single",
                Amount = 10,
                UserId = 1,
                Number = new List<int> { 5 }
            };

            // Act
            int betId = await _rouletteService.PlaceBetAsync(bet);

            // Assert
            Assert.True(betId > 0);
        }

        [Fact]
        public async Task PlaceBetAsync_InvalidBet_ThrowsInvalidOperationException()
        {
            // Arrange
            var bet = new BetRequest
            {
                BetType = "Single",
                Amount = 0, // Invalid amount
                UserId = 1,
                Number = new List<int> { 5 }
            };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _rouletteService.PlaceBetAsync(bet));
        }

        [Fact]
        public async Task SpinAsync_ReturnsSpinResult()
        {
            // Arrange & Act
            var spinResult = await _rouletteService.SpinAsync();

            // Assert
            Assert.NotNull(spinResult);
            Assert.InRange(spinResult.SpinNumber, 0, 36);
        }



        [Fact]
        public async Task SpinAsync_ShouldAddSpinResultToDatabase()
        {
            // Arrange & Act
            var spinResult = await _rouletteService.SpinAsync();

            // Assert
            Assert.NotNull(spinResult);
            Assert.True(spinResult.Id > 0);
            Assert.Single(_dbContext.SpinResults);
        }

        [Fact]
        public async Task PayoutAsync_ShouldProcessBetsAndUpdateDatabase()
        {
            // Arrange
            _dbContext.Bets.Add(new Bet { UserId = 1, BetType = BetTypeConstants.BetType.Odd, Amount = 100, IsProcessed = false });
            _dbContext.Bets.Add(new Bet { UserId = 2, BetType = BetTypeConstants.BetType.Even, Amount = 200, IsProcessed = false });
            await _dbContext.SaveChangesAsync();
            SpinResult spinResult = await _rouletteService.SpinAsync();

            // Act
            var processedBets = await _rouletteService.PayoutAsync(spinResult.Id);

            // Assert
            Assert.Equal(2, processedBets.ProcessedBetCount);
            Assert.All(processedBets.WinningBets, bet => Assert.True(bet.IsProcessed));

            // Add additional assertions for payout calculation based on bet type and spin number
        }

        [Fact]
        public async Task ShowPreviousSpinsAsync_ShouldReturnListOfPreviousSpins()
        {
            // Arrange
            _dbContext.SpinResults.Add(new SpinResult { SpinNumber = 7, ResultColour = NumberColour.Black });
            _dbContext.SpinResults.Add(new SpinResult { SpinNumber = 23, ResultColour = NumberColour.Red });
            await _dbContext.SaveChangesAsync();

            // Act
            var previousSpins = await _rouletteService.ShowPreviousSpinsAsync();

            // Assert
            Assert.Equal(2, previousSpins.Count);
            Assert.Contains(previousSpins, spin => spin.SpinNumber == 7);
            Assert.Contains(previousSpins, spin => spin.SpinNumber == 23);
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Roulette.API.Interfaces;
using Roulette.API.Models;

namespace RouletteAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RouletteController : ControllerBase
    {
        private readonly IRouletteService _rouletteService;

        public RouletteController(IRouletteService rouletteService)
        {
            _rouletteService = rouletteService;
        }

        [HttpPost("PlaceBet")]
        public async Task<IActionResult> PlaceBet([FromBody] BetRequest bet)
        {
            int id = await _rouletteService.PlaceBetAsync(bet);
            return Ok(new { Id = id });
        }

        [HttpPost("Spin")]
        public async Task<IActionResult> Spin()
        {
            SpinResult spinResult = await _rouletteService.SpinAsync();
            return Ok(spinResult);
        }

        [HttpPost("Payout")]
        public async Task<IActionResult> Payout(int spinId)
        {
            PayoutResult payout = await _rouletteService.PayoutAsync(spinId);
            return Ok(payout);
        }

        [HttpGet("ShowPreviousSpins")]
        public async Task<IActionResult> ShowPreviousSpins()
        {
            List<SpinResult> spinResults = await _rouletteService.ShowPreviousSpinsAsync();
            return Ok(spinResults);
        }
    }
}
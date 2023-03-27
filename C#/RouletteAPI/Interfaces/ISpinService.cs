using Roulette.API.Models;

namespace Roulette.API.Interfaces
{
    public interface ISpinService
    {
        Task<SpinResult> SpinAsync();
        Task<List<SpinResult>> GetSpinResultsAsync();
    }
}

using Asteroides.Models;

namespace Asteroides.Services
{
    public interface IAsterorideService
    {
        Task<IEnumerable<AsteroideDto>> GetAsteroides(int days);
    }
}

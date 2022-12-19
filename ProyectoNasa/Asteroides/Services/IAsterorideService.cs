using Asteroides.Models;

namespace Asteroides.Services
{
    public interface IAsterorideService
    {
        Task<List<Asteroide>> GetAsteroides();
    }
}

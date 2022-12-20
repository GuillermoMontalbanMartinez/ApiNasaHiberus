using System.Net.Http;
using Asteroides.Models;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Asteroides.Services
{
    public class AsteroideService : IAsterorideService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AsteroideService(IHttpClientFactory httpClienFactory, IConfiguration configuration, IMapper mapper)
        {
            this._httpClientFactory = httpClienFactory;
            this._configuration = configuration;
            this._mapper = mapper;
        }

        public async Task<List<Asteroide>> GetAsteroides()
        {
            List<Asteroide> asteroides = new();
            var cliente = this._httpClientFactory.CreateClient();
            var respuesta = await cliente.GetAsync(_configuration.GetSection("ConnectionString").GetSection("UrlApiNasa").Value);
     
            // Console.WriteLine(respuesta.Content);
            JObject json = new();
            json = JObject.Parse(await respuesta.Content.ReadAsStringAsync());
            // Console.WriteLine(json);

            var dias = json.GetValue("near_earth_objects");
            // Console.WriteLine(dias);

            foreach( var asteroide in dias.Children())
            {
                foreach(var asteroide2 in asteroide.ElementAtOrDefault(0))
                {
                    var asteroideObject = JsonConvert.DeserializeObject<Asteroide>(asteroide2.ToString());
                    asteroides.Add(asteroideObject);
                    
                }
            }

            var _ = GetAsteroidesDto(asteroides);

            return asteroides;
        }

        private List<AsteroideDto> GetAsteroidesDto(List<Asteroide> asteroides)
        {
            List<AsteroideDto> asteroidesDtos = new();

            foreach(var asteroide in asteroides)
            {
                var prueba2 = asteroide;
                var _mapperAsteroid = _mapper.Map<AsteroideDto>(asteroide);
                var prueba3 = _mapperAsteroid;
                asteroidesDtos.Add(_mapperAsteroid);
            }
            Console.WriteLine(asteroidesDtos);
            return asteroidesDtos;
        }

    }
}

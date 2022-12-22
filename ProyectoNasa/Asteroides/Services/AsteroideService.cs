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

        public async Task<IEnumerable<AsteroideDto>> GetAsteroides(int days)
        {
            string parametrosLlamadaApi = GetParametresHeaderUrl(days);
            List<Asteroide> asteroides = new();
            var cliente = this._httpClientFactory.CreateClient();
            var respuesta = await cliente.GetAsync(_configuration.GetSection("ConnectionString").GetSection("UrlApiNasa").Value + parametrosLlamadaApi.ToString());
     
            JObject json = new();
            json = JObject.Parse(await respuesta.Content.ReadAsStringAsync());

            var dias = json.GetValue("near_earth_objects");

            foreach( var asteroide in dias.Children())
            {
                foreach(var asteroide2 in asteroide.ElementAtOrDefault(0))
                {
                    var asteroideObject = JsonConvert.DeserializeObject<Asteroide>(asteroide2.ToString());
                    asteroides.Add(asteroideObject);
                }
            }

            var listasAsteroidesDto = GetAsteroidesDto(asteroides);
            var query = listasAsteroidesDto.OrderByDescending(t => t.Fecha).Take(3);
            return query;
        }

        private List<AsteroideDto> GetAsteroidesDto(List<Asteroide> asteroides)
        {
            List<AsteroideDto> asteroidesDtos = new();

            foreach(var asteroide in asteroides)
            {
                if (asteroide.IsPotentiallyHazardousAsteroid)
                {
                    try
                    {
                        var _mapperAsteroid = _mapper.Map<AsteroideDto>(asteroide);
                        asteroidesDtos.Add(_mapperAsteroid);
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }     
                
            }
            return asteroidesDtos;
        }

        private string GetParametresHeaderUrl(int day)
        {
            DateTime hoy = DateTime.Now;
            DateTime diaFin = hoy.AddDays(day);
            return "&start_date=" + hoy.Date.ToString("yyyy-MM-dd") + "&end_date=" +  diaFin.Date.ToString("yyyy-MM-dd");
        }

    }
}

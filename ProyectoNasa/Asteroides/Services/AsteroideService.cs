using System.Net.Http;
using Asteroides.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Asteroides.Services
{
    public class AsteroideService : IAsterorideService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public AsteroideService(IHttpClientFactory httpClienFactory, IConfiguration configuration)
        {
            this._httpClientFactory = httpClienFactory;
            this._configuration = configuration;
        }

        public async Task<List<Asteroide>> GetAsteroides()
        {
            List<Asteroide> asteroides = new();
            var cliente = this._httpClientFactory.CreateClient();
            var respuesta = await cliente.GetAsync(_configuration.GetSection("ConnectionString").GetSection("UrlApiNasa").Value);
     
            Console.WriteLine(respuesta.Content);
            JObject json = new();
            json = JObject.Parse(await respuesta.Content.ReadAsStringAsync());
            Console.WriteLine(json);

            var dias = json.GetValue("near_earth_objects");
            Console.WriteLine(dias);

            foreach( var asteroide in dias.Children())
            {
                foreach(var asteroide2 in asteroide.ElementAtOrDefault(0))
                {
                    var asteroideObject = JsonConvert.DeserializeObject<Asteroide>(asteroide2.ToString());
                    asteroides.Add(asteroideObject);
                }
            }

            return asteroides;
        }

    }
}

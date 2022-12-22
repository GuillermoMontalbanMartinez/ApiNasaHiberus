using Asteroides.Models;
using Asteroides.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Asteroides.Controllers
{
    /// <summary>
    /// Api de Asteroides potencialmente peligrosos
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AsteroideController : ControllerBase
    {
        private readonly IAsterorideService _asterorideService;

        public AsteroideController(IAsterorideService asteroideService) {
            this._asterorideService = asteroideService;
        }

        /// GET: /api/Asteroide/3
        /// <summary>
        /// Devuelve la lista de los 3 Asteroides potencialmente más peligrosos.
        /// </summary>
        /// <remarks>
        /// Devuelve la lista de los 3 Asteroides potencialmente mas peligrosos de la api de la Nasa en base a su diametro de longitud en un intervalo de días de 1...7 posteriores a el dia actual.
        /// </remarks>
        /// <param name="numeroDias"> Indica el número de días desde hoy para encontrar los 3 meteoritos más peligrosos  </param>
        /// <returns> D</returns>
        /// <response code="200"> Devuelve los 3 meteoritos más peligros desde el día actual hasta el número de días introducido por parámetro</response>
        /// <response code="400"> El número de días se pasa del rango (eres muy tonto ...) </response>
        /// <response code="500"> Esta la cosa fea el servidor no ha tenido respuesta :( </response>

        [HttpGet("{numeroDias:int}")]
        [ProducesResponseType(typeof(List<AsteroideDto>), 200)]
        public ActionResult Index(int numeroDias)
        {
            if (numeroDias < 0 || numeroDias > 7)
            {
                return BadRequest("El número de días tiene que ser mayor que 0 y menor que 7, el valor introducido fue: " + numeroDias);
            }

            var listaAsteroides = this._asterorideService.GetAsteroides(numeroDias);

            if (listaAsteroides is null)
            {
                return StatusCode(500);
            }
            
            return Ok(this._asterorideService.GetAsteroides(numeroDias).Result);
        }
        
    }
}
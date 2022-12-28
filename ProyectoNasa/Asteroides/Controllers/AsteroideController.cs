using Asteroides.Models;
using Asteroides.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        private readonly ILogger<AsteroideController> _logger;

        public AsteroideController(IAsterorideService asteroideService, ILogger<AsteroideController> logger) {
            this._asterorideService = asteroideService;
            this._logger = logger;
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
                // this._logger.LogInformation("El número de días introducido deber ser mayor que 0 y menor que yo, el valor introducido fue: " + numeroDias);
                _logger.LogError("Fuera de rango");
                return BadRequest();
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
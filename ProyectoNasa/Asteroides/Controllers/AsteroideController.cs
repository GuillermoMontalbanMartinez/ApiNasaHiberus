using Asteroides.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Asteroides.Controllers
{
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
        /// Devuelve la lista de los 3 Asteroides potencialmente mas peligrosos.
        /// </summary>
        /// <remarks>
        /// Devuelve la lista de los 3 Asteroides potencialmente mas peligrosos de la api de la Nasa en base a su diametro de longitud en un intervalo del 1...7 de dias posteriores a el dia actual.
        /// </remarks>
        /// <param name="numeroDias"> Indica el numero de dias que quiero para ver </param>
        /// <returns></returns>

        [HttpGet("{numeroDias:int}")]
        public ActionResult Index(int numeroDias)
        {
            if (numeroDias < 0 || numeroDias > 7)
            {
                return BadRequest("El número de días tiene que ser mayor que 0 y menor que 7, el valor introducido fue: " + numeroDias);
            }

            Console.WriteLine(numeroDias);
            //Console.WriteLine(this._asterorideService.GetAsteroides());
            return Ok(this._asterorideService.GetAsteroides());
        }
        
    }
}
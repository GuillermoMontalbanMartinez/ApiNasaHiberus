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



        /*
        [HttpGet()]
        public ActionResult GetAsteroides() 
        {
            return Ok(_asterorideService.GetAsteroides());
        }
        */
        
    }
}
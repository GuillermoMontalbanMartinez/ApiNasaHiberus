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
        private readonly IMapper _mapper;
        public AsteroideController(IAsterorideService asteroideService, IMapper mapper) {
            this._asterorideService = asteroideService;
            this._mapper = mapper;
        }

        [HttpGet("{numeroDias:int}")]
        public ActionResult Index(int numeroDias)
        {
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

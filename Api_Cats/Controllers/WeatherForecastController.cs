using Api_Cats.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api_Cats.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatController : ControllerBase
    {
        private readonly ILogger<CatController> _logger;
        private readonly ICat _cat;

        public CatController(ILogger<CatController> logger, ICat cat)
        {
            _logger = logger;
            _cat = cat;
        }

        //[HttpGet(Name = "GetCat")]
        //public IEnumerable<Cat> Get()
        //{
        //    return null;
        //}

        [HttpGet(Name = "GetCat")]
        public string Get() => _cat.Sound();       

    }
}
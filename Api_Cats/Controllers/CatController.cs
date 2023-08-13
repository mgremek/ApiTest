using Api_Cats.Entities;
using Api_Cats.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Cats.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatController : Controller
    {
        private readonly ILogger<CatController> _logger;
        private readonly ICatsService _service;

        public CatController(ILogger<CatController> logger, ICatsService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Cat>> Get() => Ok(_service.GetAll());

        [HttpGet]
        [Route("/{id}")]
        public ActionResult<Cat> Get([FromRoute] int it)
        {
            var cat = _service.Get(it);
            if (cat is not null)
                return Ok(cat);
            else
                return NotFound();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }

        [HttpPost]
        public ActionResult Create([FromBody]Cat cat) => Created($".api/cat/{_service.Create(cat)}", null);
       
    }
}

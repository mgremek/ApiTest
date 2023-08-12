using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Cats.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HumanController : Controller
    {
        private readonly ILogger<HumanController> _logger;

        public HumanController(ILogger<HumanController> logger)
        {
            _logger = logger;
        }

    }
}

using Decorator.Stores;
using Microsoft.AspNetCore.Mvc;

namespace Decorator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarStore _store;

        public CarController(ICarStore store)
        {
            _store = store;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_store.List());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_store.Get(id));
        }
    }
}

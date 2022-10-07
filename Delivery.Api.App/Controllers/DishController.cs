using Delivery.Api.DAL.Common.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DishController : ControllerBase
    {
        private readonly ILogger<DishController> _logger;

        public DishController(ILogger<DishController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetDish")]
        public IEnumerable<DishEntity> Get()
        {
            return Enumerable.Range(1, 2).Select(index => new DishEntity(
                  default, "Apple", "Fruit", 0.85, "nothing")   
            );
        }
    }
}
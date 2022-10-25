using Delivery.Api.BL.Facades;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Common.Models.Dish;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DishController : ControllerBase
    {
        private readonly ILogger<DishController> _logger;
        private readonly IDishFacade _dishFacade;

        public DishController(IDishFacade dishFacade, ILogger<DishController> logger)
        {
            _logger = logger;
            _dishFacade = dishFacade;
        }

        [HttpGet]
        public IEnumerable<DishListModel> GetAll()
        {
            return _dishFacade.GetAll();
        }

        [HttpPost]
        public ActionResult<Guid> Create(DishDetailModel dish)
        {
            return _dishFacade.Create(dish);
        }
    }
}

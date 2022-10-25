using Delivery.Api.BL.Facades;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Common.Models.Dish;
using Delivery.Common.Models.Restaurant;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly ILogger<DishController> _logger;
        private readonly IRestaurantFacade _restaurantFacade;

        public RestaurantController(IRestaurantFacade restaurantFacade, ILogger<DishController> logger)
        {
            _logger = logger;
            _restaurantFacade = restaurantFacade;
        }

        [HttpGet]
        public IEnumerable<RestaurantListModel> GetAll()
        {
            return _restaurantFacade.GetAll();
        }

        [HttpPost]
        public ActionResult<Guid> Create(RestaurantDetailModel restaurant)
        {
            return _restaurantFacade.Create(restaurant);
        }
    }
}

using Delivery.Api.BL.Facades;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Common.Models.Order;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<DishController> _logger;
        private readonly IOrderFacade _orderFacade;

        public OrderController(IOrderFacade orderFacade, ILogger<DishController> logger)
        {
            _logger = logger;
            _orderFacade = orderFacade;
        }

        [HttpGet]
        public IEnumerable<OrderListModel> GetAll()
        {
            return _orderFacade.GetAll();
        }

        [HttpPost]
        public ActionResult<Guid> Create(OrderDetailModel order)
        {
            return _orderFacade.Create(order);
        }
    }
}

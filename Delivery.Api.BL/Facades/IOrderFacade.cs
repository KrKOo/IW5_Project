using Delivery.Common.BL.Facades;
using Delivery.Common.Models.Order;

namespace Delivery.Api.BL.Facades
{
    public interface IOrderFacade : IAppFacade
    {
        List<OrderListModel> GetAll();
        OrderDetailModel? GetById(Guid id);
        Guid CreateOrUpdate(OrderDetailModel orderModel);
        Guid Create(OrderDetailModel orderModel);
        Guid? Update(OrderDetailModel orderModel);
        void Delete(Guid id);
    }
}

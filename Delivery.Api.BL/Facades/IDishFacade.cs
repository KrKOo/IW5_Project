using Delivery.Common.BL.Facades;
using Delivery.Common.Models.Dish;

namespace Delivery.Api.BL.Facades
{
    public interface IDishFacade : IAppFacade
    {
        List<DishListModel> GetAll();
        DishDetailModel? GetById(Guid id);
        Guid CreateOrUpdate(DishDetailModel dishModel);
        Guid Create(DishDetailModel dishModel);
        Guid? Update(DishDetailModel dishModel);
        void Delete(Guid id);
    }
}

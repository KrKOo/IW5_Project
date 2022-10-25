using Delivery.Common.BL.Facades;
using Delivery.Common.Models.Dish;

namespace Delivery.Api.BL.Facades
{
    public interface IDishFacade : IAppFacade
    {
        List<DishListModel> GetAll();
        DishDetailModel? GetById(Guid id);
        Guid CreateOrUpdate(DishCreateModel dishModel);
        Guid Create(DishCreateModel dishModel);
        Guid? Update(DishCreateModel dishModel);
        void Delete(Guid id);
    }
}

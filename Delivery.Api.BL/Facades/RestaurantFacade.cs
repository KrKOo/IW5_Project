using AutoMapper;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Api.DAL.Common.Repositories;
using Delivery.Common.Models.Restaurant;

namespace Delivery.Api.BL.Facades
{
    public class RestaurantFacade : IRestaurantFacade
    {
        private readonly IRestaurantRepository restaurantRepository;
        private readonly IMapper mapper;

        public RestaurantFacade(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            this.restaurantRepository = restaurantRepository;
            this.mapper = mapper;   
        }

        public Guid Create(RestaurantDetailModel restaurantModel)
        {
            var restaurantEntity = mapper.Map<RestaurantEntity>(restaurantModel);
            return restaurantRepository.Insert(restaurantEntity);
        }

        public Guid CreateOrUpdate(RestaurantDetailModel restaurantModel)
        {
            return restaurantRepository.Exists(restaurantModel.Id)
                ? Update(restaurantModel)!.Value
                : Create(restaurantModel);
        }

        public void Delete(Guid id)
        {
           restaurantRepository.Remove(id);
        }

        public List<RestaurantListModel> GetAll()
        {
            var restaurantEntities = restaurantRepository.GetAll();
            return mapper.Map<List<RestaurantListModel>>(restaurantEntities);
        }

        public RestaurantDetailModel? GetById(Guid id)
        {
            var restaurantEntity = restaurantRepository.GetById(id);
            return mapper.Map<RestaurantDetailModel>(restaurantEntity);
        }

        public Guid? Update(RestaurantDetailModel restaurantModel)
        {
            var restaurantEntity = mapper.Map<RestaurantEntity>(restaurantModel);
            return restaurantRepository.Update(restaurantEntity);
        }
    }
}

using Delivery.Api.DAL.Common.Entities;

namespace Delivery.Api.DAL.Memory
{
    public class Storage
    {
        private readonly IList<Guid> orderGuids = new List<Guid>
        {
            new ("49f20dd3-60cf-486b-968a-429f957cf272"),
            new ("ec95c734-f9b9-49d2-9f10-9954f105e906"),
            new ("d2edbf83-295c-4699-aead-aa4c8a9913e9")
        };

        private readonly IList<Guid> dishGuids = new List<Guid>
        {
            new ("27394bab-449b-4a0d-b6be-4f6a5aa8f552"),
            new ("71cd56b4-0cd3-4e4f-8f93-72b6751c7d21")
        };

        private readonly IList<Guid> dishAmountGuids = new List<Guid>
        {
            new ("cc980355-2737-44ab-b22e-c2299566c1e2"),
            new ("aaeecec3-7dc7-478f-b654-ea3176ce200f")
        };

        private readonly IList<Guid> restaurantGuids = new List<Guid>
        {
            new ("f295709e-cb91-408b-9daa-408992800954"),
            new ("cff8b2a5-2ddb-4584-b3fe-101a13956d4c"),
            new ("f7c2ec12-1b9e-451b-9e81-0524569583db")
        };

        public IList<OrderEntity> Orders { get; } = new List<OrderEntity>();
        public IList<DishEntity> Dishes { get; } = new List<DishEntity>();
        public IList<DishAmountEntity> DishAmounts { get; } = new List<DishAmountEntity>();
        public IList<RestaurantEntity> Restaurants { get; } = new List<RestaurantEntity>();

        public Storage(bool seedData = true)
        {
            if (seedData)
            {
                SeedOrders();
                SeedDishes();
                SeedDishAmounts();
                SeedRestaurants();
            }
        }

        private void SeedOrders()
        {
            Orders.Add(new OrderEntity(orderGuids[0], "94364 Rice Forest", TimeSpan.FromMinutes(40), "Faster please", restaurantGuids[0]));
            Orders.Add(new OrderEntity(orderGuids[1], "5575 Nicolas Roads", TimeSpan.FromMinutes(10), "No salad!", restaurantGuids[0]));
            Orders.Add(new OrderEntity(orderGuids[2], "303 Okuneva Passage", TimeSpan.FromMinutes(25), "Have a nice day!", restaurantGuids[1]));
        }

        private void SeedDishes()
        {
            Dishes.Add(new DishEntity(dishGuids[0], "Hod dog", "With ketchup", 3, restaurantGuids[0]));
            Dishes.Add(new DishEntity(dishGuids[1], "Tacos", "Beef tacos", 5, restaurantGuids[1]));
        }

        private void SeedDishAmounts()
        {
            DishAmounts.Add(new DishAmountEntity(dishAmountGuids[0], 3, dishGuids[0], orderGuids[1])
            {
                Id = dishAmountGuids[0]
            });

            DishAmounts.Add(new DishAmountEntity(dishAmountGuids[1], 1, dishGuids[1], orderGuids[2])
            {
                Id = dishAmountGuids[1]
            });
        }

        private void SeedRestaurants()
        {
            Restaurants.Add(new RestaurantEntity(restaurantGuids[0], "The Pub", "Just pub", "4104 Eichmann Plains", 13.67733, 163.20345));
            Restaurants.Add(new RestaurantEntity(restaurantGuids[1], "Tacos Truck", "FoodTruck", "5368 Champlin Summit", 78.531199, 15.549264));
            Restaurants.Add(new RestaurantEntity(restaurantGuids[2], "Luxury Restaurant", "Best in Town", "5389 Champlin Summit", 78.531188, 15.549250));
        }
    }
}

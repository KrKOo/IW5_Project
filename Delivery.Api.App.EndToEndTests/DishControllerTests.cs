using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Delivery.API.DAL.IntegrationTests;
using Delivery.Api.DAL.Memory;
using Delivery.Common.Enums;
using Delivery.Common.Models.Dish;
using Newtonsoft.Json;
using Xunit;
using Delivery.Common.Models.OrderDish;
using Delivery.Common.Models.Restaurant;

namespace Delivery.Api.App.EndToEndTests
{
    public class DishControllerTests : IAsyncDisposable
    {
        private readonly DeliveryApiApplicationFactory application;
        private readonly Lazy<HttpClient> client;

        public DishControllerTests()
        {
            application = new DeliveryApiApplicationFactory();
            client = new Lazy<HttpClient>(application.CreateClient());
        }

        [Fact]
        public async Task GetAllDishes_Returns_At_Last_One_Dish()
        {
            var response = await client.Value.GetAsync("/Dish");

            response.EnsureSuccessStatusCode();

            var dishes = await response.Content.ReadFromJsonAsync<ICollection<DishListModel>>();
            Assert.NotNull(dishes);
            Assert.NotEmpty(dishes);
        }
        
        [Fact]
        public async Task GetDishById_Returns_Correct_Dish()
        {
            var response = await client.Value.GetAsync("/Dish");
            response.EnsureSuccessStatusCode();

            var dishes = await response.Content.ReadFromJsonAsync<ICollection<DishDetailModel>>();
            var dishSeed = dishes.First();

            response = await client.Value.GetAsync("/Dish/" + dishSeed.Id.ToString());
            response.EnsureSuccessStatusCode();

            var dish = await response.Content.ReadFromJsonAsync<DishDetailModel>();
            Assert.NotNull(dish);
            Assert.Equal(dishSeed.Name, dish.Name);
        }
        
        [Fact]
        public async Task DeleteDishById_Delete_Correct_Dish()
        {
            var response = await client.Value.GetAsync("/Dish");
            response.EnsureSuccessStatusCode();

            var dishes = await response.Content.ReadFromJsonAsync<ICollection<DishListModel>>();
            var dishDelete = dishes.First();

            response = await client.Value.DeleteAsync("/Dish?id=" + dishDelete.Id.ToString());
            response.EnsureSuccessStatusCode();
            
            response = await client.Value.GetAsync("/Dish");
            response.EnsureSuccessStatusCode();

            var dishes2 = await response.Content.ReadFromJsonAsync<ICollection<DishListModel>>();
            var dish2 = dishes2.First();
            
            Assert.NotNull(dishes2);
            Assert.Equal(dishes.Count - 1, dishes2.Count);
            Assert.NotEqual(dishDelete.Id, dish2.Id);
        }

        [Fact]
        public async Task UpdateDish_Update_Correct_Dish()
        {
            var response = await client.Value.GetAsync("/Dish");
            response.EnsureSuccessStatusCode();

            var dishes = await response.Content.ReadFromJsonAsync<ICollection<DishDetailModel>>();
            if (dishes != null)
            {
                var updatedDish = dishes.First();
        
                updatedDish.Name = "TestDish";
                updatedDish.Description = "Testing description of 10 characters";
                updatedDish.Price = Convert.ToDecimal(150.55);
                updatedDish.Restaurant = new RestaurantListModel
                {
                    Id = new Guid(),
                    Name = "Test Name",
                    Description = "Testing description of 10 characters"
                };
                
                var json = JsonConvert.SerializeObject(updatedDish);
                HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                response = await client.Value.PatchAsync("/Dish", httpContent);     //TODO:Returning 400(Bad Request)
                response.EnsureSuccessStatusCode();

                response = await client.Value.GetAsync("/Dish/" + updatedDish.Id);
                response.EnsureSuccessStatusCode();

                var dish = await response.Content.ReadFromJsonAsync<DishDetailModel>();

                Assert.NotNull(dish);
                Assert.Equal(updatedDish.Name, dish.Name); 
            }
        }
        
        [Fact]
        public async Task CreateDish_Create_Correctly()
        {
            var createdDish = new DishCreateModel()
            {
                Id = Guid.NewGuid(),
                Name = "CreateTestRestaurant",
                Description = "CreateTest",
                Price = 4,
                RestaurantId = Guid.Parse("cff8b2a5-2ddb-4584-b3fe-101a13956d4c"),
                Allergens = new List<Allergen>() { Allergen.Fish }
            };
            
            var json = JsonConvert.SerializeObject(createdDish);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await client.Value.PostAsync("/Dish", httpContent);
            response.EnsureSuccessStatusCode();

            response = await client.Value.GetAsync("/Dish");
            response.EnsureSuccessStatusCode();
            
            var dishes = await response.Content.ReadFromJsonAsync<ICollection<DishListModel>>();
            var dish = dishes.Last();
            
            Assert.NotNull(dish);
            Assert.Equal(createdDish.Name, dish.Name);
        }

        public async ValueTask DisposeAsync()
        {
            await application.DisposeAsync();
        }
    }
}

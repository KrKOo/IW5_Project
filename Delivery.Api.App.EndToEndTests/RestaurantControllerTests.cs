using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Delivery.API.DAL.IntegrationTests;
using Delivery.Api.DAL.Memory;
using Delivery.Common.Models.Restaurant;
using Newtonsoft.Json;
using Xunit;

namespace Delivery.Api.App.EndToEndTests
{
    public class RestaurantControllerTests : IAsyncDisposable
    {
        private readonly DeliveryApiApplicationFactory application;
        private readonly Lazy<HttpClient> client;

        public RestaurantControllerTests()
        {
            application = new DeliveryApiApplicationFactory();
            client = new Lazy<HttpClient>(application.CreateClient());
        }

        [Fact]
        public async Task GetAllRestaurants_Returns_At_Last_One_Restaurant()
        {
            var response = await client.Value.GetAsync("/Restaurant");

            response.EnsureSuccessStatusCode();

            var restaurants = await response.Content.ReadFromJsonAsync<ICollection<RestaurantListModel>>();
            Assert.NotNull(restaurants);
            Assert.NotEmpty(restaurants);
        }
        
        [Fact]
        public async Task GetRestaurantById_Returns_Correct_Restaurant()
        {
            var response = await client.Value.GetAsync("/Restaurant");
            response.EnsureSuccessStatusCode();

            var restaurants = await response.Content.ReadFromJsonAsync<ICollection<RestaurantDetailModel>>();
            var restaurantSeed = restaurants.First();

            response = await client.Value.GetAsync("/Restaurant/" + restaurantSeed.Id.ToString());
            response.EnsureSuccessStatusCode();

            var restaurant = await response.Content.ReadFromJsonAsync<RestaurantDetailModel>();
            Assert.NotNull(restaurant);
            Assert.Equal(restaurantSeed.Name, restaurant.Name);
        }
        
        [Fact]
        public async Task DeleteRestaurantById_Delete_Correct_Restaurant()
        {
            var response = await client.Value.GetAsync("/Restaurant");
            response.EnsureSuccessStatusCode();

            var restaurants = await response.Content.ReadFromJsonAsync<ICollection<RestaurantListModel>>();
            var restaurantDelete = restaurants.First();

            response = await client.Value.DeleteAsync("/Restaurant?id=" + restaurantDelete.Id.ToString());
            response.EnsureSuccessStatusCode();
            
            response = await client.Value.GetAsync("/Restaurant");
            response.EnsureSuccessStatusCode();

            var restaurants2 = await response.Content.ReadFromJsonAsync<ICollection<RestaurantListModel>>();
            var restaurant2 = restaurants2.First();
            
            Assert.NotNull(restaurants2);
            Assert.Equal(restaurants.Count - 1, restaurants2.Count);
            Assert.NotEqual(restaurantDelete.Id, restaurant2.Id);
        }

        [Fact]
        public async Task UpdateRestaurant_Update_Correct_Restaurant()
        {
            var response = await client.Value.GetAsync("/Restaurant");
            response.EnsureSuccessStatusCode();

            var restaurants = await response.Content.ReadFromJsonAsync<ICollection<RestaurantDetailModel>>();
            var restaurantUpdate = restaurants.First();
            
            var updatedRestaurant = new RestaurantCreateModel()
            {
                Id = restaurantUpdate.Id,
                Name = "TestRestaurant",
                Address = restaurantUpdate.Address,
                Description = restaurantUpdate.Description,
                Latitude = restaurantUpdate.Latitude,
                Longitude = restaurantUpdate.Longitude
            };
            
            response = await client.Value.GetAsync("/Restaurant/" + updatedRestaurant.Id.ToString());
            response.EnsureSuccessStatusCode();

            var json = JsonConvert.SerializeObject(updatedRestaurant);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            response = await client.Value.PatchAsync("/Restaurant", httpContent);   //TODO:Returning 400(Bad Request)
            response.EnsureSuccessStatusCode();

            response = await client.Value.GetAsync("/Restaurant/" + updatedRestaurant.Id.ToString());
            response.EnsureSuccessStatusCode();

            var restaurant = await response.Content.ReadFromJsonAsync<RestaurantListModel>();
            Assert.NotNull(restaurant);
            Assert.Equal(updatedRestaurant.Name, restaurant.Name);
        }
        
        [Fact]
        public async Task CreateRestaurant_Create_Correctly()
        {
            var createdRestaurant = new RestaurantCreateModel()
            {
                Id = Guid.NewGuid(),
                Name = "CreateTestRestaurant",
                Address = "CreateHere",
                Description = "CreateTest",
                Latitude = 4.8,
                Longitude = 7
            };
            
            var json = JsonConvert.SerializeObject(createdRestaurant);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await client.Value.PostAsync("/Restaurant", httpContent);
            response.EnsureSuccessStatusCode();

            response = await client.Value.GetAsync("/Restaurant");
            response.EnsureSuccessStatusCode();
            
            var restaurants = await response.Content.ReadFromJsonAsync<ICollection<RestaurantListModel>>();
            var restaurant = restaurants.Last();
            
            Assert.NotNull(restaurant);
            Assert.Equal(createdRestaurant.Name, restaurant.Name);
        }

        public async ValueTask DisposeAsync()
        {
            await application.DisposeAsync();
        }
    }
}

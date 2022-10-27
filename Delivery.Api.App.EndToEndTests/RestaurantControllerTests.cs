using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Delivery.Common.Models.Restaurant;
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
            var response = await client.Value.GetAsync("/api/Restaurant");

            response.EnsureSuccessStatusCode();

            var recipes = await response.Content.ReadFromJsonAsync<ICollection<RestaurantListModel>>();
            Assert.NotNull(recipes);
            Assert.NotEmpty(recipes);
        }

        public async ValueTask DisposeAsync()
        {
            await application.DisposeAsync();
        }
    }
}

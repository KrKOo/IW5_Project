using Delivery.Common.Models.Order;
using Xunit;

namespace Delivery.Api.App.EndToEndTests
{
    public class OrderControllerTests : IAsyncDisposable
    {
        private readonly DeliveryApiApplicationFactory application;
        private readonly Lazy<HttpClient> httpClient;

        public OrderControllerTests()
        {
            application = new DeliveryApiApplicationFactory();
            httpClient = new Lazy<HttpClient>(application.CreateClient());
        }

        [Fact]
        public async Task Get_Order_Returns_Existing_Orders()
        {
            var response = await httpClient.Value.GetAsync("/Order");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadFromJsonAsync<ICollection<OrderListModel>>();
            Assert.NotNull(content);
            Assert.NotEmpty(content);
            Assert.Equal(3, content.Count);
        }

        [Fact]
        public async Task Delete_Order_Deletes_Order_Correctly()
        {

            var response = await httpClient.Value.DeleteAsync("/Order/Order_Delete/id=49f20dd3-60cf-486b-968a-429f957cf272");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadFromJsonAsync<ICollection<OrderListModel>>();
            Assert.Equal(2, content.Count);
        }

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }

    }
}

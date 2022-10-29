using System.Text;
using Delivery.Common.Enums;
using Delivery.Common.Models.Order;
using Delivery.Common.Models.Restaurant;
using Newtonsoft.Json;
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
        public async Task GetOrderById_Returns_Order_Correctly()
        {
            var response = await httpClient.Value.GetAsync("/Order");
            response.EnsureSuccessStatusCode();

            var orders = await response.Content.ReadFromJsonAsync<ICollection<OrderDetailModel>>();
            if (orders != null)
            {
                var order = orders.First();

                response = await httpClient.Value.GetAsync("/Order/" + order.Id);
                response.EnsureSuccessStatusCode();

                var orderById = await response.Content.ReadFromJsonAsync<OrderDetailModel>();

                Assert.NotNull(orderById);
                Assert.Equal(order!.Id, orderById!.Id);
            }
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
            var response = await httpClient.Value.GetAsync("/Order");
            response.EnsureSuccessStatusCode();

            var ordersBeforeDeletion = await response.Content.ReadFromJsonAsync<ICollection<OrderListModel>>();
            var firstOrderBefore = ordersBeforeDeletion!.First();

            response = await httpClient.Value.DeleteAsync("/Order?id=" + firstOrderBefore.Id);
            response.EnsureSuccessStatusCode();

            response = await httpClient.Value.GetAsync("/Order");
            response.EnsureSuccessStatusCode();

            var ordersAfterDeletion = await response.Content.ReadFromJsonAsync<ICollection<OrderListModel>>();
            var firstOrderAfter = ordersAfterDeletion!.First();

            Assert.Equal(ordersBeforeDeletion!.Count - 1, ordersAfterDeletion!.Count);
            Assert.NotEqual(firstOrderBefore!.Id, firstOrderAfter!.Id);   
        }

        [Fact]
        public async Task Create_Order_Creates_Order_Correctly()
        {
            var createdOrder = new OrderCreateModel()
            {
                Id = Guid.NewGuid(),
                Address = "Test address",
                DeliveryTime = TimeSpan.FromMinutes(30),
                Note = "Test note",
                State = OrderStates.Accepted
            };

            var json = JsonConvert.SerializeObject(createdOrder);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.Value.PostAsync("/Order", httpContent);
            response.EnsureSuccessStatusCode();

            response = await httpClient.Value.GetAsync("/Order");
            response.EnsureSuccessStatusCode();

            var orders = await response.Content.ReadFromJsonAsync<ICollection<OrderListModel>>();
            var order = orders!.Last();

            Assert.NotNull(order);
            Assert.Equal(createdOrder!.Id, order!.Id);
        }

        [Fact]
        public async Task Update_Order_Updates_Order_Correctly()
        {
            var response = await httpClient.Value.GetAsync("/Order");
            response.EnsureSuccessStatusCode();

            var orders = await response.Content.ReadFromJsonAsync<ICollection<OrderDetailModel>>();
            
            var updatedOrder = orders!.First();

            updatedOrder.Note = "Test";
            updatedOrder.Address = "Test address";
            updatedOrder.State = OrderStates.Delivered;
            updatedOrder.DeliveryTime = TimeSpan.FromMinutes(0);

            var json = JsonConvert.SerializeObject(updatedOrder);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            response = await httpClient.Value.PatchAsync("/Order", httpContent);
            response.EnsureSuccessStatusCode();

            response = await httpClient.Value.GetAsync("/Order/" + updatedOrder.Id);
            response.EnsureSuccessStatusCode();

            var order = await response.Content.ReadFromJsonAsync<OrderDetailModel>();

            Assert.NotNull(order);
            Assert.Equal(updatedOrder!.Id, order!.Id);
            Assert.Equal(updatedOrder.Note, order.Note);
            Assert.Equal(updatedOrder.Address, order.Address);
            Assert.Equal(updatedOrder.State, order.State);
            Assert.Equal(updatedOrder.DeliveryTime, order.DeliveryTime);
            
        }
        
        public async ValueTask DisposeAsync()
        {
            await application.DisposeAsync();
        }

    }
}

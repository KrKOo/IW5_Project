namespace Delivery.Web.BL
{
    public partial class RestaurantClient
    {
        public RestaurantClient(HttpClient httpClient, string baseUrl)
            : this(httpClient)
        {
            BaseUrl = baseUrl;
        }
    }
}

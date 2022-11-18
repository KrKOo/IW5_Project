namespace Delivery.Web.BL
{
    public partial class OrderClient
    {
        public OrderClient(HttpClient httpClient, string baseUrl)
            : this(httpClient)
        {
            BaseUrl = baseUrl;
        }
    }
}

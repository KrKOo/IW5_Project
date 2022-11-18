namespace Delivery.Web.BL
{
    public partial class RevenueClient
    {
        public RevenueClient(HttpClient httpClient, string baseUrl)
            : this(httpClient)
        {
            BaseUrl = baseUrl;
        }
    }
}

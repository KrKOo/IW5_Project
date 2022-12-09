namespace Delivery.Web.BL
{
    public partial class FilterClient
    {
        public FilterClient(HttpClient httpClient, string baseUrl)
            : this(httpClient)
        {
            BaseUrl = baseUrl;
        }
    }
}

namespace Delivery.Web.BL
{
    public partial class DishClient
    {
        public DishClient(HttpClient httpClient, string baseUrl) 
            : this(httpClient)
        {
            BaseUrl = baseUrl;
        }
    }
}

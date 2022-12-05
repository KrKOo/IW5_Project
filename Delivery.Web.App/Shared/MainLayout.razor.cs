using System.Threading.Tasks;
using Delivery.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

namespace Delivery.Web.App
{
    public partial class MainLayout
    {
        [Inject]
        public DishFacade DishFacade { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;


        public async Task OnlineStatusChangedAsync(bool isOnline)
        {
            if (isOnline)
            {
                var dataChanged = false;
                //!DELETE dataChanged = dataChanged || await IngredientFacade.SynchronizeLocalDataAsync();
                dataChanged = dataChanged || await DishFacade.SynchronizeLocalDataAsync();

                if (dataChanged)
                {
                    NavigationManager.NavigateTo(NavigationManager.Uri, true);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EmailApp.Services
{
    public class NavigationService : INavigationService
    {
        public Task GoBack()
        {
            return App.Current.MainPage.Navigation.PopAsync();
        }

        public Task Navigate(Page page)
        {
            return App.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}

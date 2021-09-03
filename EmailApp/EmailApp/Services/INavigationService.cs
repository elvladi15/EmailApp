using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EmailApp.Services
{
    public interface INavigationService
    {
        Task Navigate(Page page);
        Task GoBack();
    }
}

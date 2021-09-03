using EmailApp.Models;
using EmailApp.Services;
using EmailApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmailApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ComposeMailPage : ContentPage
    {
        public ComposeMailPage(ObservableCollection<Mail> mails)
        {
            InitializeComponent();
            BindingContext = new ComposeMailViewModel(mails,new AlertService(),new NavigationService());
        }
    }
}
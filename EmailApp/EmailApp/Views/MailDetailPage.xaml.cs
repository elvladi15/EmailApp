using EmailApp.Models;
using EmailApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmailApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MailDetailPage : ContentPage
    {
        public MailDetailPage(Mail mail)
        {
            InitializeComponent();
            BindingContext = new MailDetailViewModel(mail);
        }
    }
}
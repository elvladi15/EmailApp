using EmailApp.Models;
using EmailApp.Services;
using EmailApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EmailApp.ViewModels
{
    class ComposeMailViewModel:BaseViewModel
    {
        public ICommand AddMailCommand { get; }
        public ICommand PickPhotoCommand { get; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        private IAlertService _alertService;
        private INavigationService _navigationService;
        public ComposeMailViewModel(ObservableCollection<Mail> mails, IAlertService alertService,INavigationService navigationService)
        {
            _alertService = alertService;
            _navigationService = navigationService;

            AddMailCommand = new Command(async () =>
            {
                if(string.IsNullOrEmpty(Title)||
                string.IsNullOrEmpty(Description)||
                string.IsNullOrEmpty(From)||
                string.IsNullOrEmpty(To))
                {
                    await _alertService.Alert("Error", "Please fill all the fields", "OK");
                }
                else
                {
                    mails.Add(new Mail("user_image.png", Title, Description, From, To));                 
                    await _alertService.Alert("Notification", "The Mail has been sent!", "OK");
                    await _navigationService.GoBack();
                }         
            });
        }
    }
}

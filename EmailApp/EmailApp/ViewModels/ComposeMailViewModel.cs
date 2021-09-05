using EmailApp.Models;
using EmailApp.Services;
using EmailApp.Views;
using Plugin.LocalNotification;
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
        public string ImageSource { get; set; }

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
                    int listPosition = (mails.Count == 0) ? 0 : mails[mails.Count - 1].ListPosition + 1;
                    Mail createdMail = new Mail(
                        listPosition,
                        "user_image.png", 
                        Title, 
                        Description, 
                        From, 
                        To, 
                        ImageSource,
                        DateTime.Now,
                        false,
                        "unselected_star_icon.png");
                    mails.Add(createdMail);
                    await _navigationService.GoBack();
                    await _alertService.Alert("Notification", "The Mail has been sent!", "OK");

                    string[] recipients = To.Split();
                    await Email.ComposeAsync(Title, Description, recipients);

                    var notification = new NotificationRequest
                    {
                        Title = "Notification",
                        Description = "Your email has been sent!",                      
                    };
                   await NotificationCenter.Current.Show(notification);
                }         
            });
            PickPhotoCommand = new Command(PickPhoto);
        }
        private async void PickPhoto()
        {
            var photo = await MediaPicker.PickPhotoAsync();
            await LoadPhotoAsync(photo);
        }
        async Task LoadPhotoAsync(FileResult photo)
        {
            if (photo == null)
            {
                ImageSource = null;
                return;
            }
            // save the file into local storage
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);
            ImageSource = newFile;
        }
    }
}

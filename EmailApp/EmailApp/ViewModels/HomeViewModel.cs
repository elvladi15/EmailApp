using EmailApp.Models;
using EmailApp.Services;
using EmailApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EmailApp.ViewModels
{
    public class HomeViewModel:BaseViewModel
    {
        private Mail _selectedMail;
        public Mail SelectedMail
        {
            get
            {
                return _selectedMail;
            }
            set
            {
                _selectedMail = value;
                if (_selectedMail != null)
                {
                    SelectedMailCommand.Execute(_selectedMail);                  
                }
                SelectedMail = null;
            }
        }
        public ObservableCollection<Mail> Mails { get; } = new ObservableCollection<Mail>();
        private ICommand SelectedMailCommand { get; }
        public ICommand GoToComposePageCommand { get; }
        public ICommand FavoriteCommand { get; }
        public ICommand DeleteMailCommand { get; }
        private INavigationService _navigationService;
        public HomeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            SelectedMailCommand = new Command<Mail>(OnSelectedMail);
            GoToComposePageCommand = new Command(GoToComposePage);
            FavoriteCommand = new Command<Mail>(FavoriteMail);
            DeleteMailCommand = new Command<Mail>(DeleteMail);
        }
        private async void OnSelectedMail(Mail mail)
        {
            mail.FontAttribute = FontAttributes.None;
            await _navigationService.Navigate(new MailDetailPage(mail));           
        }
        private async void GoToComposePage()
        {
            await _navigationService.Navigate(new ComposeMailPage(Mails));
        }
        private void FavoriteMail(Mail mail)
        {
            mail.FavoriteStarImage = mail.IsFavorite ? "unselected_star_icon.png" : "selected_star_icon.png";
            mail.IsFavorite = !mail.IsFavorite;
        }
        private async void DeleteMail(Mail mail)
        {
            bool remove = await App.Current.MainPage.DisplayAlert("Delete mail", "Are you sure you want to delete the mail?", "yes", "no");
            if (remove)
            {
                Mails.Remove(mail);
            }      
        } 
    }
}

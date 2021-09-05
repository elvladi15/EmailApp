using EmailApp.Models;
using EmailApp.Services;
using EmailApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;

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
        public ObservableCollection<Mail> Mails { get; set; } = new ObservableCollection<Mail>();
        private ICommand SelectedMailCommand { get; }
        public ICommand GoToComposePageCommand { get; }
        public ICommand FavoriteCommand { get; }
        public ICommand DeleteMailCommand { get; }
        private INavigationService _navigationService;
        public HomeViewModel(INavigationService navigationService)
        {
           for(int i = 0; Preferences.ContainsKey("mail"+i.ToString()); i++)
            {             
                int listPosition = Preferences.Get("mail" + i.ToString(), -1);
                string userImage = Preferences.Get("mail" + i.ToString() + "_UserImage", null);
                string title = Preferences.Get("mail" + i.ToString() + "_Title", null);
                string description = Preferences.Get("mail" + i.ToString() + "_Description", null);
                string from = Preferences.Get("mail" + i.ToString() + "_From", null);
                string to = Preferences.Get("mail" + i.ToString() + "_To", null);
                string imageSource = Preferences.Get("mail" + i.ToString() + "_ImageSource", null);
                DateTime date = Preferences.Get("mail" + i.ToString() + "_Date", DateTime.Now);
                bool isFavorite = Preferences.Get("mail" + i.ToString() + "_IsFavorite", false);
                string favoriteStarImage = Preferences.Get("mail" + i.ToString() + "_FavoriteStarImage", "");              

                Mails.Add(new Mail(
                    listPosition,
                    userImage, 
                    title, 
                    description, 
                    from, 
                    to, 
                    imageSource, 
                    date, 
                    isFavorite, 
                    favoriteStarImage));
            }
            _navigationService = navigationService;

            SelectedMailCommand = new Command<Mail>(OnSelectedMail);
            GoToComposePageCommand = new Command(GoToComposePage);
            FavoriteCommand = new Command<Mail>(FavoriteMail);
            DeleteMailCommand = new Command<Mail>(DeleteMail);
        }
        private async void OnSelectedMail(Mail mail)
        {
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
            Preferences.Remove("mail" + mail.ListPosition.ToString() + "_IsFavorite");
            Preferences.Remove("mail" + mail.ListPosition.ToString() + "_FavoriteStarImage");

            Preferences.Set("mail" + mail.ListPosition.ToString() + "_IsFavorite", mail.IsFavorite);
            Preferences.Set("mail" + mail.ListPosition.ToString() + "_FavoriteStarImage", mail.FavoriteStarImage);
        }
        private async void DeleteMail(Mail mail)
        {
            bool remove = await App.Current.MainPage.DisplayAlert("Delete mail", "Are you sure you want to delete the mail?", "yes", "no");
            if (remove)
            {
                Mails.Remove(mail);
                Preferences.Clear();
                for(int i = 0; i < Mails.Count; i++)
                {
                    Mails[i].ListPosition = i;
                    AddMailPreferences(Mails[i]);
                }
            }      
        } 
        private void AddMailPreferences(Mail mail)
        {
            Preferences.Set("mail" + mail.ListPosition.ToString(), mail.ListPosition);
            Preferences.Set("mail" + mail.ListPosition.ToString() + "_UserImage", mail.UserImage);
            Preferences.Set("mail" + mail.ListPosition.ToString() + "_Title", mail.Title);
            Preferences.Set("mail" + mail.ListPosition.ToString() + "_Description", mail.Description);
            Preferences.Set("mail" + mail.ListPosition.ToString() + "_From", mail.From);
            Preferences.Set("mail" + mail.ListPosition.ToString() + "_To", mail.To);
            Preferences.Set("mail" + mail.ListPosition.ToString() + "_ImageSource", mail.ImageSource);
            Preferences.Set("mail" + mail.ListPosition.ToString() + "_Date", mail.Date);
            Preferences.Set("mail" + mail.ListPosition.ToString() + "_IsFavorite", mail.IsFavorite);
            Preferences.Set("mail" + mail.ListPosition.ToString() + "_FavoriteStarImage", mail.FavoriteStarImage);
        }
    }
}

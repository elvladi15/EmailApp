using EmailApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EmailApp.Models
{
    public class Mail: BaseViewModel
    {
        public Mail(int listPosition, string userImage, string title, string description, string from, string to, string imageSource, DateTime date, bool isFavorite, string favoriteStarImage)
        {
            ListPosition = listPosition;
            UserImage = userImage;
            Title = title;
            Description = description;
            From = from;
            To = to;
            ImageSource = imageSource;
            Date = date;
            IsFavorite = isFavorite;
            FavoriteStarImage = favoriteStarImage;
           
            Preferences.Set("mail" + listPosition, listPosition);
            Preferences.Set("mail" + listPosition + "_UserImage", userImage);
            Preferences.Set("mail" + listPosition + "_Title", Title);
            Preferences.Set("mail" + listPosition + "_Description", description);
            Preferences.Set("mail" + listPosition + "_From", from);
            Preferences.Set("mail" + listPosition + "_To", to);
            Preferences.Set("mail" + listPosition + "_ImageSource", imageSource);
            Preferences.Set("mail" + listPosition + "_Date", date);
            Preferences.Set("mail" + listPosition + "_IsFavorite", isFavorite);
            Preferences.Set("mail" + listPosition + "_FavoriteStarImage", favoriteStarImage);
        }
        public int ListPosition { get; set;}
        public string UserImage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool IsFavorite { get; set; }
        public string FavoriteStarImage { get; set; }
        public string ImageSource { get; set; }

    }
}

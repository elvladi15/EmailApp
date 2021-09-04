using EmailApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace EmailApp.Models
{
    public class Mail: BaseViewModel
    {
        public Mail(string userImage, string title, string description, string from, string to,string imageSource)
        {
            UserImage = userImage;
            Title = title;
            Description = description;
            From = from;
            To = to;
            ImageSource = imageSource;

            Date = DateTime.Now;
            FontAttribute = FontAttributes.Bold;
            IsFavorite = false;
            FavoriteStarImage = "unselected_star_icon.png";

        }
        public string UserImage { get; }
        public string Title { get; }
        public string Description { get; }
        public DateTime Date { get; }
        public string From { get; }
        public string To { get; }
        public FontAttributes FontAttribute { get; set; }
        public bool IsFavorite { get; set; }
        public string FavoriteStarImage { get; set; }
        public string ImageSource { get; set; }

    }
}

using EmailApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailApp.ViewModels
{
    public class MailDetailViewModel
    {
        public string UserImage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool IsFavorite { get; set; }
        public string FavoriteStarImage { get; set; }
        public MailDetailViewModel(Mail mail)
        {
            UserImage = mail.UserImage;
            Title = mail.Title;
            Description = mail.Description;
            Date = mail.Date;
            From = mail.From;
            To = "To " + mail.To;
            IsFavorite = mail.IsFavorite;
            FavoriteStarImage = mail.FavoriteStarImage;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailApp.Services
{
    public interface IAlertService
    {
        Task Alert(string title, string message, string cancel);
    }
}

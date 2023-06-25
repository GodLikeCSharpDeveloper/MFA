using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFA.Services.NotificationService
{
    internal class NotificationService : INotificationService
    {
        public async Task Notify(string title, string message, string btnText)
        {
            await Shell.Current.DisplayAlert(title, message, btnText);
        }
        public async Task Notify(string message, string btnText)
        {
            await Shell.Current.DisplayAlert("Error", message, btnText);
        }
        public async Task Notify(string message)
        {
            await Shell.Current.DisplayAlert("Error", message, "OK");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFA.Services.NotificationService
{
    public interface INotificationService
    {
        public async Task Notify(string title, string message, string btnText)
        {
            throw new NotImplementedException();
        }
        public async Task Notify(string message, string btnText)
        {
            throw new NotImplementedException();
        }
        public async Task Notify(string message)
        {
            throw new NotImplementedException();
        }
    }
}

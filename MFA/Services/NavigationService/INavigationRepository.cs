using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFA.Services.NavigationService
{
    public interface INavigationRepository
    {
        public async Task NavigateTo(string page)
        {
            throw new NotImplementedException();
        }
        public async Task NavigateTo(string page, bool animate)
        {
            throw new NotImplementedException();
        }
        public async Task WaitingNavigateTo(string page, bool animate)
        {
            throw new NotImplementedException();
        }
        public async Task NavigateTo(string page, Dictionary<string, object> data)
        {
            throw new NotImplementedException();
        }
    }
}

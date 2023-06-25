using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFA.Services.NavigationService
{
    public class NavigationRepository : INavigationRepository
    {
        public async Task NavigateTo(string page)
        {
            await Shell.Current.GoToAsync(page, true);
        }
        public async Task NavigateTo(string page, bool animate)
        {
            await Shell.Current.GoToAsync(page, animate);
        }
        public async Task WaitingNavigateTo(string page, bool animate)
        {
            await Shell.Current.GoToAsync(page, animate).WaitAsync(new CancellationToken());
        }

        public async Task NavigateTo(string page, Dictionary<string, object> data)
        {
            await Shell.Current.GoToAsync(page, true, data);
            
        }
    }
}

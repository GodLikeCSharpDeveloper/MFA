using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFA.Services.DBService;

namespace MFA.Utility.UiHelper
{
    public interface IDbChecker
    {
        public async Task Waits()
        {
        }

        public async void Check()
        {
            throw new NotImplementedException();
        }
    }
}

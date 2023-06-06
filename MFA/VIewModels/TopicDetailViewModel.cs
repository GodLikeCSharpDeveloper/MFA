using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFA.ViewModels
{
    [QueryProperty(nameof(Topic),"Topic")]
    public partial class TopicDetailViewModel : BaseViewModel
    {
        public TopicDetailViewModel()
        {
            
        }
        [ObservableProperty]
        Topic topic;
    }
}

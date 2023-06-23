using MFA.Services.DBService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFA.ViewModels
{
    public partial class TopicAddOrRemoveViewModel : BaseViewModel
    {
        ITopicDBService topicDBService;

        public TopicAddOrRemoveViewModel(ITopicDBService topicDBService)
        {
            this.topicDBService = topicDBService;
        }
        [ObservableProperty]
        public string topicTitle;
        [ObservableProperty]
        public string topicContent;
        [ObservableProperty]
        public string topicReleaseDate;

        [RelayCommand]
        public async Task AddNewTopic()
        {
            var topic = new Topic
            {
                TopicTitle = TopicTitle,
                TopicContent = TopicContent,
                TopicReleaseDate = TopicReleaseDate,
                OwnerId = RealmService.app.CurrentUser.Id
            };
            await topicDBService.AddNewTopic(topic);
        }
    }
}

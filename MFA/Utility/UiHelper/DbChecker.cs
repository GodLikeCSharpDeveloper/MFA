
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFA.Services.TopicService;

namespace MFA.Utility.UiHelper
{
    public class DbChecker : IDbChecker
    {
        ITopicDBService topicDbService;
        MainPageViewModel mainPageViewModel;
        public DbChecker(ITopicDBService topicDbService, MainPageViewModel mainPageViewModel)
        {
            this.topicDbService = topicDbService;
            this.mainPageViewModel = mainPageViewModel;
        }
        public async Task Waits()
        {
            while (topicDbService.GetAllTopics().Count == 0)
            {
                Thread.Sleep(100);
            }
        }
        public async void Check()
        {
            if (mainPageViewModel.Topics != null && topicDbService.GetAllTopics().Count == 0 && mainPageViewModel.Topics.Count > 0)
            {
                mainPageViewModel.Topics.Clear();
                mainPageViewModel.IsBusy = true;
                await Task.Factory.StartNew(Waits);

            }
            mainPageViewModel.IsBusy = false;
            var all = topicDbService.GetAllTopics().Take(30);
            mainPageViewModel.Topics = new(all);

        } 
    }
}

using System.Collections.ObjectModel;
using MauiForumApp.Services;
using MFA.Models;

namespace MFA.ViewModels
{
    public class MainPageViewModel
    {
        public ObservableCollection<Topic> Topics { get; }
        private TopicService service;
        public MainPageViewModel(TopicService service)
        {
            this.service = service;
            Topics = new( service.GenerateInfo(25));
        }
    }
}

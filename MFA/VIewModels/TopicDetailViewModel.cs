using MFA.Services.DBService;
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
        [ObservableProperty] 
        List<UsersComment> usersComments = new();

        [RelayCommand]
        public void AddNewComment()
        {
            var comment = new UsersComment
            {
                Content = "Testing comments lulw",
                CreationDate = "20.20.2020",
                Topic = topic,
                User = MainPageViewModel.User
            };
            var realm = RealmService.GetRealm();
            realm.Write(() =>
            {
                realm.Add<UsersComment>(comment);
            });
            
           
            UsersComments.Add(comment);
        }
    }
}

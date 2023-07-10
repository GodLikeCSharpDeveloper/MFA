using MFA.Services.DBService;
using MFA.Services.UsersCommentsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Bogus;
using MFA.Utility.UiHelper.CollectionUiLogic;

namespace MFA.ViewModels
{
    [QueryProperty(nameof(Topic), "Topic")]
    public partial class TopicDetailViewModel : BaseViewModel
    {
        public List<UsersComment> _usersComments;
        IUsersCommentService usersCommentService;
        ICollectionUiLogic<UsersComment> collectionUiLogic;
        public TopicDetailViewModel(IUsersCommentService usersCommentService, ICollectionUiLogic<UsersComment> collectionUiLogic)
        {
            this.usersCommentService = usersCommentService;
            this.collectionUiLogic = collectionUiLogic;
            _usersComments = usersCommentService.GetAllCurrentTopicComments(this.Topic).ToList();
            
        }
        [ObservableProperty]
        public Topic topic;

        
        [ObservableProperty]
        ObservableCollection<UsersComment> usersComments;

        [ObservableProperty] 
        UsersComment usersComment = new();
        [RelayCommand]
        public async Task AddNewComment()
        {
            var newComment = new UsersComment
            {
                Content = usersComment.Content,
                CreationDate = DateTime.Now.ToString(),
                Topic = this.Topic,
            };
            List<UsersComment> GenerateInfo(int number)
            {
                var faker = new Faker<UsersComment>().RuleFor(p => p.Content, f => f.Lorem.Paragraphs(1, 10, "/n"))
                                                     .RuleFor(p => p.UpdateDate, f => f.Date.FutureDateOnly().ToShortDateString()).
                                                     RuleFor(x=>x.Topic, f=>this.Topic);
                return faker.Generate(number).ToList();
                //TODO DATABASE CONTENT POG POG PogChamp
            }

            var newComment2 = GenerateInfo(50);
            foreach (var item in newComment2)
            {
                await usersCommentService.AddNewComment(item);
            }
            
            //UsersComments.Add(newComment);
        }
        int commentsCount = 30;
        public ICommand OnCollectionEndReachedCommand => new Command(OnCollectionEndReached);
        [ObservableProperty] public string numberoftimes = "0";
        void OnCollectionEndReached()
        {
            collectionUiLogic.OnCollectionEndReached(_usersComments, UsersComments, ref commentsCount);
            var a = Convert.ToInt32(numberoftimes);
            a++;
            numberoftimes = a.ToString();
        }
    }
}

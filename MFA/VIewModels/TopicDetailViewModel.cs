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
using MFA.Services.LikeRepository;

namespace MFA.ViewModels
{

    [QueryProperty(nameof(Topic), "Topic")]
    public partial class TopicDetailViewModel : BaseViewModel
    {
        public static List<UsersComment> _usersComments;
        IUsersCommentService usersCommentService;
        ICollectionUiLogic<UsersComment> collectionUiLogic;
        ILikeRepository likeRepository;
        public TopicDetailViewModel(IUsersCommentService usersCommentService, ICollectionUiLogic<UsersComment> collectionUiLogic, ILikeRepository likeRepository)
        {
            this.usersCommentService = usersCommentService;
            this.collectionUiLogic = collectionUiLogic;
            this.likeRepository = likeRepository;
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
                                                     RuleFor(x => x.Topic, f => this.Topic);
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
        [ObservableProperty]
        public bool animateImg;
        
        public int commentsCount = 30;
        public ICommand OnCollectionEndReachedCommand => new Command(OnCollectionEndReached);
        void OnCollectionEndReached()
        {
            collectionUiLogic.OnCollectionEndReached(_usersComments, UsersComments, ref commentsCount);
        }

        [RelayCommand]
        async Task LikeComment(object parameter)
        {
            var comment = (UsersComment)parameter;
            comment.LikeStatus = "icons8like24black.png";
            OnPropertyChanged(nameof(comment.LikeStatus));
            await likeRepository.AddNewLike(comment);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

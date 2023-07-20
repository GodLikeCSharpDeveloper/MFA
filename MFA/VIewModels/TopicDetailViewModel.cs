using System.Windows.Input;
using Bogus;
using MFA.Services.LikeRepository;
using MFA.Services.NavigationService;
using MFA.Services.UsersCommentsService;
using MFA.Utility.UiHelper.CollectionUiLogic;
using MFA.Views;

namespace MFA.ViewModels
{

    [QueryProperty(nameof(Topic), "Topic")]
    public partial class TopicDetailViewModel : BaseViewModel
    {
        public static List<CommentWrapper> _usersComments;
        IUsersCommentService usersCommentService;
        ICollectionUiLogic<CommentWrapper> collectionUiLogic;
        ILikeRepository likeRepository;
        INavigationRepository navigationRepository;
        public TopicDetailViewModel(IUsersCommentService usersCommentService, ICollectionUiLogic<CommentWrapper> collectionUiLogic, ILikeRepository likeRepository, INavigationRepository navigationRepository)
        {
            this.usersCommentService = usersCommentService;
            this.collectionUiLogic = collectionUiLogic;
            this.likeRepository = likeRepository;
            this.navigationRepository = navigationRepository;
        }
        [ObservableProperty]
        public Topic topic;

        [ObservableProperty]
        ObservableCollection<CommentWrapper> usersComments = new();

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
                UsersComments.Add(new CommentWrapper(item));
            }
            //UsersComments.Add(newComment);
        }

        public int commentsCount = 30;
        public ICommand OnCollectionEndReachedCommand => new Command(OnCollectionEndReached);
        void OnCollectionEndReached()
        {
            collectionUiLogic.OnCollectionEndReached(_usersComments, UsersComments, ref commentsCount);
        }

        [RelayCommand]
        async Task GoToEditComment(Object obj)
        {
            var comment = (CommentWrapper)obj;
            await navigationRepository.NavigateTo(nameof(CommentEditPage), new Dictionary<string, object>()
            {
                {"Comment", comment.usersComment}
            });
        }
        [RelayCommand]
        async Task LikeComment(object parameter)
        {
            var comment = (CommentWrapper)parameter;
            var com = UsersComments.FirstOrDefault(x => x.UsersComment._id == comment.UsersComment._id).LikeStatus;
            if (com != "icons8like24black.png")
            {
                comment.LikeStatus = "icons8like24black.png";
                await likeRepository.AddNewLike(comment.UsersComment);
            }
            else
            {
                comment.LikeStatus = "icons8like.png";
                await likeRepository.RemoveLike(comment.UsersComment);
            }
        }
    }
}

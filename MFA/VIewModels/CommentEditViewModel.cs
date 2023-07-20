namespace MFA.ViewModels
{
    [QueryProperty("Comment", "Comment")]
    public partial class CommentEditViewModel : BaseViewModel
    {
        [ObservableProperty]
        private UsersComment comment;

        public CommentEditViewModel()
        {
            
        }

        [RelayCommand]
        void Check()
        {
            var a = Comment;
        }

    }
}

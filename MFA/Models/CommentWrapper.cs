using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFA.Models
{
   
    public partial class CommentWrapper : ObservableObject
    {
        [ObservableProperty]
        public UsersComment usersComment;
        public CommentWrapper(UsersComment usersComment)
        {
            this.usersComment = usersComment;
        }
        [ObservableProperty]
        public string likeStatus = "icons8like.gif";
    }
}

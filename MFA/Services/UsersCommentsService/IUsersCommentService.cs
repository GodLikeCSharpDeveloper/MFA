using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFA.Services.UsersCommentsService
{
    public interface IUsersCommentService
    {
        public async Task AddNewComment(UsersComment usersComment)
        {
            throw new NotImplementedException();
        }

        public List<CommentWrapper> GetAllCurrentTopicComments(Topic topic);

        public async Task<UsersComment> GetCommentById(UsersComment comment)
        {
            throw new NotImplementedException();
        }

        public async Task<UsersComment> UpdateCommentAsync(UsersComment comment)
        {
            throw new NotImplementedException();
        }
    }
}

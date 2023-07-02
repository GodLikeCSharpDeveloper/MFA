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

        public List<UsersComment> GetAllCurrentTopicComments(Topic topic);
    }
}

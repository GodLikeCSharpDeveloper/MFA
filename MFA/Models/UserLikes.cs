using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using Realms;

namespace MFA.Models
{
    public partial class UserLikes : IRealmObject
    {
        [PrimaryKey]
        public ObjectId _id { get; set; } = ObjectId.GenerateNewId();
        public User OwnerUser { get; set; }
        public UsersComment LikedComments { get; set; }
        public Topic LikedTopics { get; set; }
    }
}

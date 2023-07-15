using MongoDB.Bson;
using Realms;
using MFA.Services.DBService;

namespace MFA.Models
{
    public partial class User : IRealmObject
    {
        [PrimaryKey]
        [MapTo("_id")]
        public ObjectId _id { get; set; } = ObjectId.GenerateNewId();
        [MapTo("ids")]
        public string OwnerId { get; set; }
        [MapTo("name")]
        public string Name { get; set; }
        [MapTo("email")]
        public string Email { get; set; }
        [MapTo("password")]
        public string Password { get; set; }
        [MapTo("address")]
        public string Address { get; set; }
        [MapTo("Avatar")]
        public ImageData? UsersImage { get; set; }
        public IList<UsersComment> UsersComments { get; }
        public IList<UserLikes> UserLikes { get; }

    }
}

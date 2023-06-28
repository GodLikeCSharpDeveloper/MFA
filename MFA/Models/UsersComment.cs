using MongoDB.Bson;
using Realms;
using MFA.Services.DBService;

namespace MFA.Models
{
    public partial class UsersComment : IRealmObject
    {
        [PrimaryKey]
        [MapTo("_id")]
        public ObjectId _id { get; set; } = ObjectId.GenerateNewId();
        public string Content { get; set; }
        public string CreationDate { get; set; }
        public string UpdateDate { get; set; }
        public Topic Topic { get; set; }
        public User User { get; set; }
    }
}

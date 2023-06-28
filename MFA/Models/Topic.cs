using Realms;
using MongoDB.Bson;


namespace MFA.Models
{
    public partial class Topic : IRealmObject
    {
        [PrimaryKey]
        [MapTo("_id")]
        public ObjectId _id { get; set; } = ObjectId.GenerateNewId();
        [MapTo("ids")]
        public string OwnerId { get; set; }
        public string TopicTitle { get; set; }
        public string TopicContent { get; set; }
        public string TopicReleaseDate { get; set; }
        public string TopicUpdateDate { get; set; }
        public IList<UsersComment> UsersComments { get; }
    }
}

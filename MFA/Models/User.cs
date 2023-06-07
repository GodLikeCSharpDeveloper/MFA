using MongoDB.Bson;
using Realms;
using MFA.Services.DBService;

namespace MFA.Models
{
    public partial class User : IRealmObject
    {
        [PrimaryKey]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public bool IsMine => OwnerId == RealmService.CurrentUser.Id;
    }
}

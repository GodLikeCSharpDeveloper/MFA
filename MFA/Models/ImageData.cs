using MongoDB.Bson;
using Realms;

namespace MFA.Models
{
    public partial class ImageData : IRealmObject
    {
        [PrimaryKey]
        public ObjectId _id { get; set; } = ObjectId.GenerateNewId();
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public User user { get; set; }
        public byte[]? Data { get; set; }
        [Ignored]
        public ImageSource imgSource
        {
            get
            {
                MemoryStream stream = new MemoryStream(Data);
                return ImageSource.FromStream(() => stream);
            }
        }

    }
}

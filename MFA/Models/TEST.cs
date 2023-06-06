using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MFA.Models
{
    [BsonDiscriminator("TEST")]
    public class TEST
    {
        [BsonElement("TEST2")]
        public string TEST2 { get; set; }
        [BsonId]
        public ObjectId Id { get; set; }

    }
}

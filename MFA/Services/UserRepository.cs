using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel.Communication;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MFA.Services
{
    
    public class UserRepository
    {
        public UserRepository(MongoClient mongoClient)
        {
        }

        public async Task<bool> AddUser()
        {
            var connstring = "mongodb+srv://TestUser:TestUser@cluster0.3gzbzss.mongodb.net/";
            var conn = new MongoClient(connstring);
            var col = conn.GetDatabase("sample_mfix").GetCollection<BsonDocument>("MYUSERS");
            BsonDocument tom = new BsonDocument
            {
                {"Name", "Tom"},
                {"Age", 38}
            };
            col.InsertOne(tom);
            return true;
        }
        
    }
}

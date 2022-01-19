using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShopsNearByy.Models
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }


    }
}

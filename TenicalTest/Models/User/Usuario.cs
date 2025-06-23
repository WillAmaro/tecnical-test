using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlazorTecnicalTest.Models.User
{

    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("passwordHash")]
        public string PasswordHash { get; set; }
    }
}

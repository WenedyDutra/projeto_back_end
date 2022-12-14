using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectEllevo.API.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Cpf { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}

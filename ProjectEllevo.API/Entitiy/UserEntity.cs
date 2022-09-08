﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectEllevo.API.Entitiy
{
    public class UserEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Cpf { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}

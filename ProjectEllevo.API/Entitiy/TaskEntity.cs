using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ProjectEllevo.API.Entitiy
{
    public class TaskEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Generator { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Responsible { get; set; }
    }
}

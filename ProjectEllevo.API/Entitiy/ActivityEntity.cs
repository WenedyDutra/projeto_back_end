using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectEllevo.API.Entitiy
{
    public class ActivityEntity
    {
        [BsonId]
        public ObjectId Id{ get; set; }
        public string Title { get; set; }
        public string TaskId { get; set; }
    }
}

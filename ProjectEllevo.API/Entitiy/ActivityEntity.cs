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
        public ObjectId TaskId { get; set; }
        public ObjectId ActivityId { get; set; }
        public string ActivityTitle { get; set; }
        public string ActivityDescription { get; set; }
    }
}

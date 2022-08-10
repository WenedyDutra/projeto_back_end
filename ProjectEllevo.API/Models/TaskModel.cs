using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectEllevo.API.Models
{
    public class TaskModel
    {
        public string Id { get; set; }
        public string Generator { get; set; }
        public  string Title { get; set; }
        public string Discription { get; set; }
        public string? Responsible { get; set; }
        public ICollection<ActivityModel>? Activities { get; set; }
    }
}

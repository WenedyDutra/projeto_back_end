using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using ProjectEllevo.API.Entitiy;
using ProjectEllevo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectEllevo.API.Services
{
    public class ActivityAppService
    {
        //public readonly IUserService UserService {get; set;}
        private readonly IMongoCollection<ActivityEntity> _user;
        //private readonly string key;
        public ActivityAppService(ImongoDbdatabaseSettings settings, IConfiguration configuration)
        {

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _user = database.GetCollection<ActivityEntity>(settings.UserCollectionName);

            //this.key = configuration.GetSection("JwtKey").ToString();
        }
        public List<ActivityEntity> GetAllActivity() =>
            _user.Find(ActivityEntity => true).ToList();

        public ActivityEntity GetActivityId(ObjectId id) =>
            _user.Find<ActivityEntity>(ActivityEntity => ActivityEntity.Id == id).FirstOrDefault();


        public ActivityEntity CreateActivity(ActivityEntity ActivityModel)
        {
            _user.InsertOne(ActivityModel);
            return ActivityModel;
        }
        public void UpdateActivity(ObjectId id, ActivityEntity activityModel) =>
            _user.ReplaceOne(ActivityEntity => ActivityEntity.Id == id, activityModel);

        public void RemoveActivity(ActivityEntity activityModel) =>
            _user.DeleteOne(ActivityEntity => ActivityEntity.Id == activityModel.Id);

        public void RemoveActivity(ObjectId id) =>
            _user.DeleteOne(ActivityEntity => ActivityEntity.Id == id);

    }
}

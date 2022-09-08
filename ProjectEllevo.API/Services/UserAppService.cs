using AutoMapper;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using ProjectEllevo.API.Entitiy;
using ProjectEllevo.API.Models;
using System.Collections.Generic;
using System.Linq;



namespace ProjectEllevo.API.Services
{
    public class UserAppService
    {
        //public readonly IUserService UserService {get; set;}
        private readonly IMongoCollection<UserEntity> _user;
        //private readonly string key;
         public UserAppService(ImongoDbdatabaseSettings settings, IConfiguration configuration)
        {

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _user = database.GetCollection<UserEntity>(settings.UserCollectionName);

            //this.key = configuration.GetSection("JwtKey").ToString();
        }
        public List<UserEntity> GetAllUser() =>
            _user.Find(UserEntity => true).ToList();

        public UserEntity GetLogin(string userName, string password) =>
            _user.Find(UserEntity => UserEntity.UserName == userName && UserEntity.Password == password).FirstOrDefault();

        public UserEntity GetId(ObjectId id) =>
            _user.Find<UserEntity>(UserEntity => UserEntity.Id == id).FirstOrDefault();

        public UserEntity GetUserByName(string userName) =>
            _user.Find<UserEntity>(LoginEntity => LoginEntity.UserName == userName).FirstOrDefault();
        //public UserEntity GetUserByPassword(string password) =>
        //    _user.Find<UserEntity>(LoginEntity => LoginEntity.UserName == userName).FirstOrDefault();

        public string GetName(ObjectId userId)
        {
            return GetId(userId).Name;
        }

        public UserEntity Create(UserEntity UserModel)
        {
            _user.InsertOne(UserModel);
            return UserModel;
        }
        public void Update(ObjectId id, UserEntity userIn) =>
            _user.ReplaceOne(UserEntity => UserEntity.Id == id, userIn);

        public void Remove(UserEntity userIn) =>
            _user.DeleteOne(UserEntity => UserEntity.Id == userIn.Id);

        public void Remove(ObjectId id) =>
            _user.DeleteOne(UserEntity => UserEntity.Id == id);
    }
}

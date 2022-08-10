using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using ProjectEllevo.API.Entitiy;
using ProjectEllevo.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEllevo.API.Services
{
    public class TaskAppService
    {
        private readonly IMongoCollection<TaskEntity> _task;

        public TaskAppService(ImongoDbdatabaseSettings settings, IConfiguration configuration)
        {

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _task = database.GetCollection<TaskEntity>(settings.TaskCollectionName);

            //this.key = configuration.GetSection("JwtKey").ToString();
        }


        public List<TaskEntity> GetAllTask() =>
            _task.Find(TaskEntity => true).ToList();

        public TaskEntity GetTaskId(ObjectId id) =>
            _task.Find<TaskEntity>(TaskEntity => TaskEntity.Id == id).FirstOrDefault();

        public TaskEntity CreateTask(TaskEntity taskModel)
        {
            _task.InsertOne(taskModel);
            return taskModel;
        }
        public void UpdateTask(ObjectId id, TaskEntity taskModel) =>
            _task.ReplaceOne(taskModel => taskModel.Id == id, taskModel);

        public void RemoveTask(TaskEntity taskModel) =>
            _task.DeleteOne(taskModel => taskModel.Id == taskModel.Id);

        public void RemoveTask(ObjectId id) =>
            _task.DeleteOne(TaskEntity => TaskEntity.Id == id);
    }
}

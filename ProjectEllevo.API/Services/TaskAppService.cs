using AutoMapper;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using ProjectEllevo.API.Entitiy;
using ProjectEllevo.API.Enum;
using ProjectEllevo.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEllevo.API.Services
{
    public class TaskAppService
    {
        private readonly IMongoCollection<TaskEntity> _task;
        private readonly IMongoCollection<ActivityEntity> _activity;
        private readonly IMapper _mapper;

        public TaskAppService(ImongoDbdatabaseSettings settings, IConfiguration configuration, IMapper mapper)
        {

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _mapper = mapper;
            _task = database.GetCollection<TaskEntity>(settings.TaskCollectionName);
            _activity = database.GetCollection<ActivityEntity>(settings.TaskCollectionName);

        }

        public List<TaskEntity> GetAllTask() =>
            _task.Find(TaskEntity => true).ToList();

        public TaskEntity GetTaskId(ObjectId id) =>
            _task.Find<TaskEntity>(TaskEntity => TaskEntity.Id == id).FirstOrDefault();

        public void CreateActivity(ActivityModel activityModel)
        {
            var task = GetTaskId(ObjectId.Parse(activityModel.TaskId));
            var model = _mapper.Map<TaskEntity, TaskModel>(task);
            var entity = _mapper.Map<ActivityModel, ActivityEntity>(activityModel);
            entity.ActivityId = ObjectId.GenerateNewId();
            task.Activities.Add((entity));
            UpdateTask(ObjectId.Parse(model.Id), task);
        }

        public TaskEntity CreateTask(TaskEntity taskModel)
        {
            _task.InsertOne(taskModel);
            return taskModel;
        }
        public void UpdateTask(ObjectId id,TaskEntity taskModel) =>
            _task.ReplaceOne(taskModel => taskModel.Id == id, taskModel);

        public void RemoveTask(TaskEntity taskModel) =>
            _task.DeleteOne(taskModel => taskModel.Id == taskModel.Id);

        public void RemoveTask(ObjectId id) =>
            _task.DeleteOne(TaskEntity => TaskEntity.Id == id);

        public string GetStatusDescription(EStatus status)
        {
            switch (status)
            {
                case EStatus.NaoIniciado:
                    {
                        return "Não Iniciado";
                    }
                case EStatus.EmAndamento:
                    {
                        return "Em Andamento";
                    }
                case EStatus.Aguardando:
                    {
                        return "Aguardando";
                    }
                case EStatus.Concluido:
                    {
                        return "Concluído";
                    }
            }
            return "";
        }
    }

}

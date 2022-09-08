using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ProjectEllevo.API.Entitiy;
using ProjectEllevo.API.Models;
using ProjectEllevo.API.Services;
using System.Collections.Generic;

namespace ProjectEllevo.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly TaskAppService _taskAppService;
        private readonly IMapper _mapper;
        private readonly UserAppService _userAppService;
        public TaskController(TaskAppService taskAppService, IMapper mapper, UserAppService userAppService)
        {
            _mapper = mapper;
            _taskAppService = taskAppService;
            _userAppService = userAppService;
        }

        [HttpGet("lisTask")]
        public ActionResult<List<TaskModel>> GetTask()
        {
            var result = _taskAppService.GetAllTask();
            var results = _mapper.Map<List<TaskEntity>, List<TaskModel>>(result);
            foreach (var task in results)
            {
                task.StatusDescription = _userAppService.GetName(ObjectId.Parse(task.Responsible));
                task.Generator = _userAppService.GetName(ObjectId.Parse(task.Generator));
            }
            return Ok(results);
        }

        [HttpGet("{id:length(24)}")]
        public ActionResult<TaskModel> GetId(string id)
        {
            var task = _taskAppService.GetTaskId(ObjectId.Parse(id));
            var results = _mapper.Map<TaskEntity, TaskModel>(task);
            return Ok(results);
        }

        [HttpPost("createTask")]
        public ActionResult<TaskModel> CreateTask(TaskModel task)
        {
            var results = _mapper.Map<TaskModel, TaskEntity>(task);
            _taskAppService.CreateTask(results);
            return Ok(new { message = "Tarefa criada com sucesso !" });
        }

        [HttpPut("updateTask")]
        public IActionResult Update([FromBody] TaskModel taskModel)
        {
            var task = _taskAppService.GetTaskId(ObjectId.Parse(taskModel.Id));
            taskModel.Generator = task.Generator;
            var model = _mapper.Map(taskModel, task);
            _taskAppService.UpdateTask(ObjectId.Parse(taskModel.Id), model);
            return Ok(new { message = "Usuário atualizado com sucesso !" });
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult DeleteTask(string id)
        {
            var user = _taskAppService.GetTaskId(ObjectId.Parse(id));
            _taskAppService.RemoveTask(ObjectId.Parse(id));
            return Ok(new { message = "Usuário excluído com sucesso !" });
        }

        [HttpPut("createActivity")]
        public IActionResult CreateActivity([FromBody] ActivityModel activityModel)
        {
            _taskAppService.CreateActivity(activityModel);
            return Ok(new { message = "Usuário atualizado com sucesso !" });
        }
    }
}

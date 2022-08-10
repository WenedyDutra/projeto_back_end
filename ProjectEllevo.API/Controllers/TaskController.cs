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

        public TaskController(TaskAppService taskAppService, IMapper mapper)
        {
            _mapper = mapper;
            _taskAppService = taskAppService;
        }

        [HttpGet("lisTask")]
        public ActionResult<List<TaskModel>> GetTask()
        {
            var result = _taskAppService.GetAllTask();

            //_mapper.Map<TaskEntity, TaskModel>(result);

            //return NoContent();
            return Ok(new { result });
        }

        [HttpGet("{id:length(24)}")]
        public ActionResult<TaskModel> GetId(ObjectId id)
        {
            var user = _taskAppService.GetTaskId(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(new { user });
        }

        [HttpPost("")]
        public ActionResult<TaskModel> CreateTask(TaskEntity task)
        {
            _taskAppService.CreateTask(task);

            return CreatedAtRoute("GetTask", new { id = task.Id.ToString() }, task);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult UpdateTask(string id, [FromBody] TaskModel taskModel)
        {
            var user = _taskAppService.GetTaskId(ObjectId.Parse(id));

            if (user == null)
            {
                return NotFound();
            }
            var model = _mapper.Map(taskModel, user);
            _taskAppService.UpdateTask(ObjectId.Parse(id), model);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult DeleteTask(string id)
        {
            var user = _taskAppService.GetTaskId(ObjectId.Parse(id));

            if (user == null)
            {
                return NotFound();
            }

            _taskAppService.RemoveTask(ObjectId.Parse(id));

            return NoContent();
        }

    }
}

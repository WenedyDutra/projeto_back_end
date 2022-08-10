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
    public class ActivityController : ControllerBase
    {
        private readonly ActivityAppService _activityAppService;
        private readonly IMapper _mapper;

        public ActivityController(ActivityAppService activityAppService, IMapper mapper)
        {
            _mapper = mapper;
            _activityAppService = activityAppService;
        }
        //listar todas as atividades
        [HttpGet("listActivity")]
        public ActionResult<List<ActivityModel>> Get()
        {
            var result = _activityAppService.GetAllActivity();

            return Ok(new { result });
        }

        [HttpGet("getActivityId")]
        public ActionResult<ActivityModel> Get(ObjectId id)
        {
            var user = _activityAppService.GetActivityId(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(new { user });
        }

        [HttpPost("createActivity")]
        public ActionResult<UserModel> createActivity(ActivityEntity activiry)
        {
            _activityAppService.CreateActivity(activiry);

            return CreatedAtRoute("GetUser", new { id = activiry.Id.ToString() }, activiry);
        }

        //[HttpPut("{id:length(24)}")]
        [HttpPut("updateActivity{id:length(24)}")]
        public IActionResult Update(string id, [FromBody] ActivityModel activityModel)
        {
            var activity = _activityAppService.GetActivityId(ObjectId.Parse(id));

            if (activity == null)
            {
                return NotFound();
            }
            var model = _mapper.Map(activityModel, activity);
            _activityAppService.UpdateActivity(ObjectId.Parse(id), model);

            return NoContent();
        }

        //[HttpDelete("{id:length(24)}")]
        [HttpDelete("deleteActivity{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var activity = _activityAppService.GetActivityId(ObjectId.Parse(id));

            if (activity == null)
            {
                return NotFound();
            }

            _activityAppService.RemoveActivity(ObjectId.Parse(id));

            return NoContent();
        }
    }
}

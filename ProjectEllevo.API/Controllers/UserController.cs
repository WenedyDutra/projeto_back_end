using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ProjectEllevo.API.Models;
using ProjectEllevo.API.Services;
using AutoMapper;
using System.Collections.Generic;
using ProjectEllevo.API.Entitiy;

namespace ProjectEllevo.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserAppService _userService;
        private readonly IMapper _mapper;

        public UserController(UserAppService userService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
        }
        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous]
        public ActionResult Authenticate([FromBody] UserModel model)
        {
            var user = TokenAppService.GenerateToken(model.UserName, model.Password);
            if (user == null)
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            }
            //var token = TokenServiceAppService.GenerateToken(user);
            return Ok(new { user });
        }
        //[AllowAnonymous]
        //[Route("authenticate")]
        //[HttpPost]
        //public ActionResult Login([FromBody] UserModel userModel)
        //{
        //    var token = _userService.Authenticate(userModel.UserName, userModel.Password);

        //    if (token == null)
        //    {
        //        return Unauthorized();
        //    }
        //    return Ok(new { token, userModel });
        //}

        [HttpGet("listUser")]
        public ActionResult<List<UserModel>> Get()
        {
            var result = _userService.GetAllUser();

            return Ok(new { result });
        }

        //[HttpGet("getUser/{id:length(24)}", Name = "GetUser")]
        [HttpGet("getUserId/{id:length(24)}")]
        public ActionResult<UserModel> Get(string id)
        {
            var user = _userService.GetId(ObjectId.Parse(id));

            if (user == null)
            {
                return NotFound();
            }

            return Ok(new { user });
        }

        [HttpPost("createUser")]
        public ActionResult<UserModel> Create(UserEntity user)
        {
            _userService.Create(user);

            return CreatedAtRoute("GetUser", new { id = user.Id.ToString() }, user);
        }

        //[HttpPut("{id:length(24)}")]
        [HttpPut("update{id:length(24)}")]
        public IActionResult Update(string id, [FromBody] UserModel userIn)
        {
            var user = _userService.GetId(ObjectId.Parse(id));

            if (user == null)
            {
                return NotFound();
            }
            var model = _mapper.Map(userIn, user);
            _userService.Update(ObjectId.Parse(id), model);

            return NoContent();
        }

        //[HttpDelete("{id:length(24)}")]
        [HttpDelete("delete{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var user = _userService.GetId(ObjectId.Parse(id));

            if (user == null)
            {
                return NotFound();
            }

            _userService.Remove(ObjectId.Parse(id));

            return NoContent();
        }
        //[AllowAnonymous]
        //[Route("authenticate")]
        //[HttpPost]
        //public ActionResult Login([FromBody] UserModel userModel)
        //{
        //    var token = _userService.Authenticate(userModel.UserName, userModel.Password);

        //    if (token == null)
        //    {
        //        return Unauthorized();
        //    }
        //    return Ok(new { token, userModel });
        //}
    }
}
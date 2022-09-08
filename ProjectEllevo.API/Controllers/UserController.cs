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
        public ActionResult<UserModel> Authenticate([FromBody] UserModel model)
        {
            var login = _userService.GetLogin(model.UserName, model.Password);
            var logins = _mapper.Map<UserEntity, UserModel>(login);
            if (logins == null)
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            }
            var token = TokenAppService.GenerateToken(model.UserName, model.Password);
            _userService.GetUserByName(model.UserName);
            var results = _mapper.Map<UserModel, UserModel>(model);
            return Ok(new { message = "Usuário logado com sucesso !" , token, results.Id, results
            .Name});
        }

        [HttpGet("listUser")]
        public ActionResult<List<UserModel>> Get()
        {
            var result = _userService.GetAllUser();
            var results = _mapper.Map<List<UserEntity>, List<UserModel>>(result);
            return Ok( results );
        }

        [HttpGet("getUserId/{id:length(24)}")]
        public ActionResult<UserModel> Get(string id)
        {
            var user = _userService.GetId(ObjectId.Parse(id));
            var users = _mapper.Map<UserEntity, UserModel>(user);
            return Ok(new { users });
        }

        [HttpPost("createUser")]
        public ActionResult<UserModel> Create(UserModel user)
        {
            var users = _mapper.Map<UserModel, UserEntity>(user);
            _userService.Create(users);
            return Ok (new { message = "Usuário criado com sucesso !" });
        }

        [HttpPut("updateUser")]
        public IActionResult Update([FromBody] UserModel userIn)
        {
            var user = _userService.GetId(ObjectId.Parse(userIn.Id));
            var model = _mapper.Map(userIn, user);
            _userService.Update(ObjectId.Parse(userIn.Id), model);
            return Ok(new { message = "Usuário atualizado com sucesso !" });
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            _userService.Remove(ObjectId.Parse(id));
            return Ok(new { message = "Usuário excluido com sucesso !" });
        }
       
    }
}
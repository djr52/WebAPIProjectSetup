using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Contracts;
using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using ActionFilters;

namespace SchoolAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]

    public class UserController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggingManager _logger;
        private readonly IMapper _mapper;

        public UserController(IRepositoryManager repository, ILoggingManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;

        }
        [HttpGet(Name = "getAllUsers")]
        public IActionResult GetUsers()
        {
            var users = _repository.User.GetAllUsers(trackChanges: false);

            var userDto = _mapper.Map<IEnumerable<UserDto>>(users);

            return Ok(userDto);

        }
        [HttpGet("{id}", Name = "getUserById")]
        [ServiceFilter(typeof(ValidateUserExistsAttribute))]
        public IActionResult GetUser(Guid id)
        {

             var user = HttpContext.Items["user"] as User;
             var userDto = _mapper.Map<UserDto>(user);
             return Ok(userDto);


        }

        [HttpPost(Name = "createUser")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateUser([FromBody] UserForCreationDto user)
        {

            var userEntity = _mapper.Map<User>(user);

            _repository.User.CreateUser(userEntity);
            _repository.Save();

            var userToReturn = _mapper.Map<UserDto>(userEntity);

            return CreatedAtRoute("getUserById", new { id = userToReturn.Id }, userToReturn);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateUserExistsAttribute))]
        public IActionResult UpdateUser(Guid id, [FromBody] UserForUpdateDto user)
        {

            var userEntity = HttpContext.Items["user"] as User;

            _mapper.Map(user, userEntity);
            _repository.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateUserExistsAttribute))]
        public IActionResult DeleteUser(Guid id)
        {
            var user = HttpContext.Items["user"] as User;

            _repository.User.DeleteUser(user);
            _repository.Save();

            return NoContent();
        }




    }
}

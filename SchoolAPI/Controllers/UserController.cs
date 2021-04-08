using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Contracts;
using AutoMapper;
using Entities.DataTransferObjects;

namespace SchoolAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
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
        [HttpGet]

        public IActionResult GetUsers()
        {
            try
            {
                var users = _repository.User.GetAllUsers(trackChanges: false);
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetUsers)} action {ex}");
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpGet("{id}")]
        public IActionResult GetUser(Guid id)
        {
            try

            {
                var user = _repository.User.GetUser(id, trackChanges: false);
                if (user == null)
                {
                    _logger.LogError($"Organization with ID: {id} doesn't exist in the database");
                    return NotFound();
                }
                else
                {
                    var userDto = _mapper.Map<UserDto>(user);
                    return Ok(userDto);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetUser)} action {ex}");
                return StatusCode(500, "Internal server error");
            }

        }




    }
}

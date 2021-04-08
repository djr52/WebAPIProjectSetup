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
    [Route("api/organizations")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggingManager _logger;
        private readonly IMapper _mapper;

        public OrganizationController(IRepositoryManager repository, ILoggingManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;

        }
        [HttpGet]

        public IActionResult GetOrganizations()
        {
            try
            {
                var organizations = _repository.Organization.GetAllOrganizations(trackChanges: false);
                return Ok(organizations);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetOrganizations)} action {ex}");
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpGet("{id}")]
        public IActionResult GetOrganization(Guid id)
        {
            try

            {
                var organization = _repository.Organization.GetOrganization(id, trackChanges: false);
                if(organization == null)
                {
                    _logger.LogError($"Organization with ID: {id} doesn't exist in the database");
                    return NotFound();
                }
                else
                {
                    var organizationDto = _mapper.Map<OrganizationDto>(organization);
                    return Ok(organizationDto);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetOrganizations)} action {ex}");
                return StatusCode(500, "Internal server error");
            }

        }





    }
}


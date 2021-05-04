using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Contracts;
using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using ActionFilters;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace SchoolAPI.Controllers
{
    [Route("api/organizations")]
    [ApiController]
    [ApiExplorerSettings(GroupName ="v1")]
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
        [HttpGet(Name = "getAllOrganizations"), Authorize]
        public IActionResult GetOrganizations([FromQuery] OrganizationParameters orgParameters)
        {
            var organizationsFromDb = _repository.Organization.GetAllOrganizations(orgParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination",
                 JsonConvert.SerializeObject(organizationsFromDb.MetaData));

            var organizationDto = _mapper.Map<IEnumerable<OrganizationDto>>(organizationsFromDb);
            //throw new Exception("Exception");
            return Ok(organizationDto);
            
        }
        
        [HttpGet("{id}", Name = "getOrganizationById")]
        [ServiceFilter(typeof(ValidateOrganizationExistsAttribute))]

        public IActionResult GetOrganization(Guid id)
        {
            var organization = HttpContext.Items["organization"] as Organization;

            var organizationDto = _mapper.Map<OrganizationDto>(organization);
            return Ok(organizationDto);
            
            
        }

        [HttpPost(Name = "createOrganization")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateOrganization([FromBody] OrganizationForCreationDto organization)
        {
            var organizationEntity = _mapper.Map<Organization>(organization);

            _repository.Organization.CreateOrganization(organizationEntity);
            _repository.Save();

            var organizationToReturn = _mapper.Map<OrganizationDto>(organizationEntity);

            return CreatedAtRoute("getOrganizationById", new { id = organizationToReturn.Id }, organizationToReturn);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateOrganizationExistsAttribute))]
        public IActionResult UpdateOrganization(Guid id, [FromBody] OrganizationForUpdateDto organization)
        {

            var organizationEntity = HttpContext.Items["organization"] as Organization;

            _mapper.Map(organization, organizationEntity);
            _repository.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateOrganizationExistsAttribute))]
        public IActionResult DeleteOrganization(Guid id)
        {
            var organization = HttpContext.Items["organization"] as Organization;

            _repository.Organization.DeleteOrganization(organization);
            _repository.Save();

            return NoContent();
        }




    }
}


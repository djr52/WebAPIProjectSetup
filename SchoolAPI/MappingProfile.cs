﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Models;
using Entities.DataTransferObjects;

namespace SchoolAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<Organization, OrganizationDto>()
                .ForMember(c => c.FullAddress,
                    opt => opt.MapFrom(x => string.Join(' ', x.City, x.Country)));
            
        }
    }
}

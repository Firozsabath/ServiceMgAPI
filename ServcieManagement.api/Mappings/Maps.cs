﻿using AutoMapper;
using ServiceManagement.Domain.Entities;
using ServiceManagement.WebAPI.DTOs;

namespace ServiceManagement.WebAPI.Mappings
{
    public class Maps: Profile
    {
        public Maps()
        {
            CreateMap<CompanyDTO, Companies>().ReverseMap();
            CreateMap<TechniciansDTO, Technicians>().ReverseMap();
        }
    }
}

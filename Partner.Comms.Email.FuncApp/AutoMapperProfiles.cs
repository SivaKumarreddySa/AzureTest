using AutoMapper;
using Partner.Comms.Common;
using Partner.Comms.DTO.Models;
using System;

namespace Partner.Comms.SMS.FuncApp
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(
            )
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
        }
    }
}
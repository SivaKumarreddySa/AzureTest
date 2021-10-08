using AutoMapper;
using Partner.Comms.Common;
using Partner.Comms.DTO;
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
            CreateMap<SMSTopicRequestDTO, SMSEndAPIRequestDTO>()
              .ForMember(dest => dest.mobile, opt => opt.MapFrom(src => src.PhoneNumber))
              .ForMember(dest => dest.message, opt => opt.MapFrom(src => src.Message));
        }
    }
}
using AutoMapper;
using Challenge.Application.DTOs;
using Challenge.Domain.Entities;

namespace Challenge.WebApi.Mappings {
    public class MappingProfile : Profile {

        public MappingProfile() {
            CreateMap<Permission, PermissionDto>().ReverseMap();
            CreateMap<PermissionType, PermissionTypeDto>().ReverseMap();
        }

    }
}

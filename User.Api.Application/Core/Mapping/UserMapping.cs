using AutoMapper;
using User.Api.Application.Core.Application.Users.Command;
using User.Api.Contracts.DTOs;
using User.Api.Domain.Entities;

namespace User.Api.Application.Core.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<UserEntity, UserDto>();
            CreateMap<UserCreateCmd, UserEntity>();
        }
    }
}

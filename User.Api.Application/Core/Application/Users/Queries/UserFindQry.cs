using AutoMapper;
using MediatR;
using System.Net;
using User.Api.Contracts.DTOs;
using User.Api.Domain.Entities;
using User.Api.Domain.Repositories;
using User.Api.Shared.Uitls;

namespace User.Api.Application.Core.Application.Users.Queries
{
    public class UserFindQry : IRequest<ResponseApi<UserDto>>
    {
        public string UserName { get; set; }
        public int Id { get; set; }
    }

    public class UserFindQryHandler : IRequestHandler<UserFindQry, ResponseApi<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserFindQryHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ResponseApi<UserDto>> Handle(UserFindQry request, CancellationToken cancellationToken)
        {
            try
            {
                var userEntity = new UserEntity();

                if (!string.IsNullOrEmpty(request.UserName))
                    userEntity = await _userRepository.GetbyUserName(request.UserName.Trim());

                if (request.Id > 0)
                    userEntity = await _userRepository.GetByIdAsync(request.Id);

                var userDto = _mapper.Map<UserDto>(userEntity);

                return ResponseApi<UserDto>.Success(userDto);
            }
            catch (Exception ex)
            {
                return ResponseApi<UserDto>.Fail(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

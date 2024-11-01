using AutoMapper;
using MediatR;
using System.Net;
using User.Api.Contracts.DTOs;
using User.Api.Domain.Repositories;
using User.Api.Shared.Uitls;

namespace User.Api.Application.Core.Application.Users.Queries
{
    public class UserGetListQry : IRequest<ResponseApi<IEnumerable<UserDto>>>
    {
    }

    public class UserGetListQryHandler : IRequestHandler<UserGetListQry, ResponseApi<IEnumerable<UserDto>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserGetListQryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ResponseApi<IEnumerable<UserDto>>> Handle(UserGetListQry request, CancellationToken cancellationToken)
        {
            try
            {
                var userList = await _userRepository.GetAllAsync();
                var userListDto = _mapper.Map<IEnumerable<UserDto>>(userList);

                return ResponseApi<IEnumerable<UserDto>>.Success(userListDto);
            }
            catch (Exception ex)
            {
                return ResponseApi<IEnumerable<UserDto>>.Fail(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

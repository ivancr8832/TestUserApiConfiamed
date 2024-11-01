using AutoMapper;
using FluentValidation;
using MediatR;
using System.Net;
using User.Api.Contracts.DTOs;
using User.Api.Domain.Entities;
using User.Api.Domain.Repositories;
using User.Api.Shared.Uitls;

namespace User.Api.Application.Core.Application.Users.Command
{
    public class UserCreateCmd : IRequest<ResponseApi<UserDto>>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class UserCreateCmdValidator : AbstractValidator<UserCreateCmd>
    {
        public UserCreateCmdValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
        }
    }

    public class UserCreateHandler : IRequestHandler<UserCreateCmd, ResponseApi<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserCreateHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ResponseApi<UserDto>> Handle(UserCreateCmd request, CancellationToken cancellationToken)
        {
            try
            {
                var user = new UserEntity();

                user = await _userRepository.GetbyUserName(request.UserName.Trim());

                if (user is null)
                {
                    user = _mapper.Map<UserEntity>(request);
                    user.Password = Security.Encrypt(request.Password);
                    await _userRepository.AddAsync(user);
                }

                return ResponseApi<UserDto>.Success(_mapper.Map<UserDto>(user));
            }
            catch (Exception ex)
            {
                return ResponseApi<UserDto>.Fail(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Net;
using User.Api.Domain.Repositories;
using User.Api.Shared.Uitls;

namespace User.Api.Application.Core.Application.Users.Command
{
    public class UserLoginCmd : IRequest<ResponseApi<string>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginCmdValidator : AbstractValidator<UserLoginCmd>
    {
        public UserLoginCmdValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }

    public class UserLoginCmdHandler : IRequestHandler<UserLoginCmd, ResponseApi<string>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserLoginCmdHandler(IMapper mapper, IUserRepository userRepository, IConfiguration configuration)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<ResponseApi<string>> Handle(UserLoginCmd request, CancellationToken cancellationToken)
        {
            try
            {
                var userByLoginAndPassword = await _userRepository.GetUserByUserNameAndPassword(request.UserName.Trim(), request.Password.Trim());

                if (userByLoginAndPassword is null)
                    return ResponseApi<string>.Fail("User or Password are incorrect", HttpStatusCode.Unauthorized);

                var key = _configuration["Jwt:Key"];

                var token = Security.GenerateJWT(userByLoginAndPassword, key);

                return ResponseApi<string>.Success(token);
            }
            catch (Exception ex)
            {
                return ResponseApi<string>.Fail(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

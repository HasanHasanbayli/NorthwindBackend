using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.DTOs.User;

namespace Business.Abstract;

public interface IAuthService
{
    IDataResult<User> Register(RegisterRequest registerRequest, string password);

    IDataResult<User> Login(AuthenticationRequest authenticationRequest);

    IResult UserExists(string email);

    IDataResult<AccessToken> CreateAccessToken(User user);
}
using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.DTOs.User;

namespace Business.Concrete;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly ITokenHelper _tokenHelper;

    public AuthService(IUserService userService, ITokenHelper tokenHelper)
    {
        _userService = userService;
        _tokenHelper = tokenHelper;
    }
    
    public IDataResult<User> Register(RegisterRequest registerRequest, string password)
    {
        HashingHelper.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

        var user = new User
        {
            Email = registerRequest.Email,
            FirstName = registerRequest.FirstName,
            LastName = registerRequest.LastName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Status = true
        };

        _userService.AddUser(user);

        return new SuccessDataResult<User>(user, Messages.UserRegistered);
    }
    
    [LogAspect(typeof(FileLogger))]
    public IDataResult<User> Login(AuthenticationRequest authenticationRequest)
    {
        var userToCheck = _userService.GetByMail(authenticationRequest.Email);

        if (userToCheck == null)
        {
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        if (!HashingHelper.VerifyPasswordHash(authenticationRequest.Password, userToCheck.PasswordHash,
                userToCheck.PasswordSalt))
        {
            return new ErrorDataResult<User>(Messages.PasswordError);
        }

        return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
    }

    public IResult UserExists(string email)
    {
        if (_userService.GetByMail(email) != null)
        {
            return new ErrorResult(Messages.UserAlreadyExists);
        }

        return new SuccessResult();
    }

    public IDataResult<AccessToken> CreateAccessToken(User user)
    {
        var claims = _userService.GetClaims(user);

        var accessToken = _tokenHelper.CreateToken(user, claims);

        return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
    }
}
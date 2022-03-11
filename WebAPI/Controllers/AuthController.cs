using Business.Abstract;
using Entities.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login(AuthenticationRequest authenticationRequest)
    {
        var userToLogin = _authService.Login(authenticationRequest);
        
        if (!userToLogin.Success)
        {
            return BadRequest(userToLogin.Message);
        }

        var result = _authService.CreateAccessToken(userToLogin.Data);

        if (result.Success)
        {
            return Ok(result.Data);
        }

        return BadRequest(result.Message);
    }
    
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest registerRequest, string password)
    {
        var userExists = _authService.UserExists(registerRequest.Email);
        
        if (!userExists.Success)
        {
            return BadRequest(userExists.Message);
        }

        var result = _authService.CreateAccessToken(userToLogin.Data);

        if (result.Success)
        {
            return Ok(result.Data);
        }

        return BadRequest(result.Message);
    }
}
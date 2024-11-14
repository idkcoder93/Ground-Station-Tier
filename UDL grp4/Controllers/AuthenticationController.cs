//using Microsoft.AspNetCore.Mvc;

//[Route("api/[controller]")]
//[ApiController]
//public class AuthenticationController : ControllerBase
//{
//    private readonly UserAuthenticationService _authService;

//    public AuthenticationController(UserAuthenticationService authService)
//    {
//        _authService = authService;
//    }

//    [HttpPost("login")]
//    public IActionResult Login([FromBody] User user)
//    {
//        if (_authService.Authenticate(user))
//        {
//            return Ok("Login successful");
//        }
//        return Unauthorized("Invalid credentials");
//    }
//}
using Microsoft.AspNetCore.Mvc;
using Model;
using MyDAL;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtTokenHelper _jwtTokenHelper;
    private readonly IStudentRepo _studentRepo;

    public AuthController(JwtTokenHelper jwtTokenHelper, IStudentRepo studentRepo)
    {
        _jwtTokenHelper = jwtTokenHelper;
        _studentRepo = studentRepo;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] Students request)
    {
        // Validate user credentials using Dapper (via _studentRepo)
        var user = _studentRepo.ValidateUser(request.username, request.password); // Add ValidateUser to IStudentRepo
        if (user == null)
        {
            return Unauthorized("Invalid credentials.");
        }

        // Generate JWT
        var token = _jwtTokenHelper.GenerateToken(request.username);
        return Ok(new { Token = token });
    }
}


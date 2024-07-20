using CodeRank.API.Identity.TokenHelpers;
using CodeRank.Application.Instructors.CreateTeacher;
using CodeRank.Application.Students.CreateTeacher;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CodeRank.API.Identity.Controllers
{
    public class AccountController : ControllerBase
    {


        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMediator _mediator;
        public AccountController(
            IMediator mediator,
            UserManager<ApplicationUser> userManager, 
            ITokenService tokenService)
        {
            _userManager = userManager; 
            _tokenService = tokenService;
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                RefreshToken = string.Empty
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // Create Instructor or Student entity and set the role
                switch (model.AccountType)
                {
                    case AccountType.Instructor:
                        var createInstructorCommand = new CreateInstructorCommand(model.Username, model.Email);
                        var instructorResult = await _mediator.Send(createInstructorCommand);
                        if (instructorResult.IsSuccess)
                        {
                            user.InstructorId = instructorResult.Value;
                            await _userManager.AddToRoleAsync(user, "Instructor");
                        }
                        else
                        {
                            // Handle error
                            return BadRequest(new RegistrationResponse { Errors = new[] { "Failed to create Instructor entity" } });
                        }
                        break;

                    case AccountType.Student:
                        var createStudentCommand = new CreateStudentCommand(model.Username, model.Email);
                        var studentResult = await _mediator.Send(createStudentCommand);
                        if (studentResult.IsSuccess)
                        {
                            user.StudentId = studentResult.Value;
                            await _userManager.AddToRoleAsync(user, "Student");
                        }
                        else
                        {
                            // Handle error
                            return BadRequest(new RegistrationResponse { Errors = new[] { "Failed to create Student entity" } });
                        }
                        break;

                    default:
                        return BadRequest(new RegistrationResponse { Errors = new[] { "Invalid account type" } });
                }

                await _userManager.UpdateAsync(user);
                return Ok(new RegistrationResponse { IsSuccessfulRegistration = true });
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponse { Errors = errors });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized(new AuthResponse { ErrorMessage = "Invalid Authentication" });
            var signingCredentials = _tokenService.GetSigningCredentials();
            var claims = await _tokenService.GetClaims(user);
            var tokenOptions = _tokenService.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);


            user.RefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(user);
            return Ok(new AuthResponse { IsAuthSuccessful = true, Token = token, RefreshToken = user.RefreshToken });



        }
    }
}

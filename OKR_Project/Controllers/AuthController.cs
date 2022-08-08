using API.DTO;
using API.DTO.UserDTO;
using API.Settings;
using AutoMapper;
using Core.Auth;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signManager;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
        private readonly IUserService _userService;
        private readonly IEmailSender _emailSender;

        public AuthController(IMapper mapper, UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signManager, IOptionsSnapshot<JwtSettings> jwtSettings, IUserService userService, IEmailSender emailSender)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _signManager = signManager;
            _jwtSettings = jwtSettings.Value;
            _userService = userService;
            _emailSender = emailSender;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            var usersResources = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);

            return Ok(usersResources);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(UserSignUpDTO userSignUpResource)
        {
            var user = _mapper.Map<UserSignUpDTO, User>(userSignUpResource);
            var userCreateResult = await _userManager.CreateAsync(user, userSignUpResource.Password);

            if (userCreateResult.Succeeded)
            {
                return Created(string.Empty, string.Empty);
            }

            return Problem(userCreateResult.Errors.First().Description, null, 500);
        }

        [HttpPut("EditUser")]
        public async Task<IActionResult> EditUser([FromBody] UpdateUserDTO saveUserResource, string userEmail)
        {
            var userToBeUpdated = await _userManager.FindByEmailAsync(userEmail);
            if (userToBeUpdated == null)
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "User does not exists!" });

            var user = _mapper.Map<UpdateUserDTO, User>(saveUserResource);
            await _userService.UpdateUser(userToBeUpdated, user);
            var updatedUser = await _userManager.FindByEmailAsync(userEmail);
            var updatedUserResource = _mapper.Map<User, UpdateUserDTO>(updatedUser);
            return Ok(updatedUserResource);
        }

        [HttpPost("Signin")]
        public async Task<IActionResult> SignIn(UserLoginDTO userLoginDto)
        {
            var user = _userService.GetAllUsers().SingleOrDefault(u => u.UserName == userLoginDto.Email);
            if (user is null)
            {
                return NotFound("User not found");
            }

            var userSigninResult = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);

            if (userSigninResult)
            {
                var role = await _userManager.GetRolesAsync(user);

                return Ok(new
                {
                    Jwt = GenerateJwt(user, role),
                    Name = user.FirstName,
                    Surname = user.LastName,
                    UserRole = user.Role.Name,
                    UserTeamName = user?.Team?.Name,
                    UserDepartmentName = user?.Team?.Department?.Name
                    //UserDepartmentName = _userService.GetDepartmentOfUser(user.Id) --> Dogru ve calisiyor
                });
            }

            return BadRequest(new Response { Status = "Success", Message = "Email or password incorrect." });
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
            return Ok(new Response { Status = "Success", Message = "Logout success!" });
        }

        [HttpPost("Roles")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Role name should be provided.");
            }

            var newRole = new Role
            {
                Name = roleName,
            };

            var roleResult = await _roleManager.CreateAsync(newRole);

            if (roleResult.Succeeded)
            {
                return Ok(new Response { Status = "Success", Message = "Role is Created Successfully!" });
            }

            return Problem(roleResult.Errors.First().Description, null, 500);
        }

        [HttpPost("ResetPasswordToken")]
        public async Task<IActionResult> ResetPasswordToken([FromBody] ResetPasswordTokenDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "User does not exists!" });
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var message = new Message(new string[] { "atasmustafa2000@gmail.com" }, "Password Changing", token);
            await SendEmailAsync(message);

            return Ok();
        }

        [HttpPost("ResetPasswordUser")]
        public async Task<IActionResult> ResetPasswordUser([FromBody] ResetPasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            if (string.Compare(model.NewPassword, model.ConfirmNewPassword) != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "The new password and confirm new password does not match!" });
            }

            if (string.IsNullOrEmpty(model.Token))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Invalid Token!"});
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (!result.Succeeded)
            {
                var errors = new List<string>();

                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = String.Join(",", errors) });
            }

            return Ok(new Response { Status = "Success", Message = "Password Reseted Successfully!" });
        }

        private async Task SendEmailAsync(Message message)
        {
            await _emailSender.SendEmailAsync(message);
        }

        private string GenerateJwt(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpirationInDays));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
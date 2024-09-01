using BusinessDomain.DTOs;
using BusinessDomain.Exceptions;
using BusinessDomain.Interfaces;
using BusinessDomain.Services;
using Infrastructure.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Presentation.Helpers;
using Presentation.Responses;
using Presentation.ViewModels;
using System.Data.Common;
using System.Runtime.InteropServices;


namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        // Constructor injection of UserService
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/users/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserViewModel CustomerVM)
        {
            BaseResponse response;
            if (!ModelState.IsValid || !CustomerVM.Email.IsEmail() || !CustomerVM.Password.IsStrong())
            {
                var errorsDict = ModelState.ToErrorDictionary();
                if (!errorsDict.ContainsKey("Email") && !CustomerVM.Email.IsEmail())
                {
                    errorsDict.Add("Email", "Invalid Email Format");
                }
                if (!errorsDict.ContainsKey("Password") && !CustomerVM.Password.IsStrong())
                {
                    errorsDict.Add("Password", "Weak Password");
                }
                response = new ErrorResponse
                {
                    StatusCode = 400,
                    Message = "Invalid Data",
                    Errors = errorsDict
                };
                return BadRequest(response);
            }
            try
            {
                UserDTO userDTO = await _userService.RegisterUserAsync(CustomerVM.Adapt<UserDTO>());
                UserViewModel data = userDTO.Adapt<UserViewModel>();
                response = new SuccessResponse<UserViewModel>
                {
                    StatusCode = 200,
                    Message = "Customer Added Successfully",
                    Data = data
                };
                return Ok(response);
            }
            catch (EmailAlreadyExistsException e)
            {
                var errorsDict = new Dictionary<string, string>
                {
                    { "Email", e.Message }
                };
                response = new ErrorResponse
                {
                    StatusCode = 400,
                    Message = "InValid Data",
                    Errors = errorsDict
                };
                return BadRequest(response);
            }
            catch (Exception e) when (e is NoSavedChangesException || e is DbException || e is DbUpdateException)
            {
                response = new BaseResponse
                {
                    StatusCode = 400,
                    Message = "Can't Add Customer",
                };
                return BadRequest(response);
            }
        }

        // POST: api/users/login/email/password
        [HttpPost("login/{Email}/{Password}")]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            BaseResponse response;
            try
            {
                UserDTO userDTO = await _userService.UserLoginAsync(Email, Password);
                UserViewModel data = userDTO.Adapt<UserViewModel>();
                response = new SuccessResponse<UserViewModel>
                {
                    StatusCode = 200,
                    Message = "Login Successfully",
                    Data = data
                };
                return Ok(response);
            }
            catch (WrongEmailException e)
            {
                response = new BaseResponse
                {
                    StatusCode = 404,
                    Message = "Email Doesn’t Exist",
                };
                return NotFound(response);
            }
            catch (WrongPasswordException e)
            {
                response = new BaseResponse
                {
                    StatusCode = 404,
                    Message = "Password Is Wrong",
                };
                return NotFound(response);
            }
            // TODO: Check this exception
            catch (DbUpdateException)
            {
                response = new BaseResponse
                {
                    StatusCode = 400,
                    Message = "Could Not Login",
                };
                return BadRequest(response);
            }
        }
    }
}

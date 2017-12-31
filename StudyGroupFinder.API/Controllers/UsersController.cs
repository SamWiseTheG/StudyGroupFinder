using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using StudyGroupFinder.API.Utilities;
using StudyGroupFinder.Common.Models;
using StudyGroupFinder.Common.Requests;
using StudyGroupFinder.Common.Responses;
using StudyGroupFinder.Data.Repositories;
using System.ComponentModel.DataAnnotations;

namespace StudyGroupFinder.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IConfiguration _configuration;
        private UsersRepository _usersRepository;

        public UsersController(IConfiguration configuration, UsersRepository usersRepository)
        {
            _configuration = configuration;
            _usersRepository = usersRepository;
        }

        #region POST api/users/signup

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<BaseResponse> Create([FromBody]SignupRequest request)
        {
            var response = new BaseResponse();

            if (request == null || !ModelState.IsValid)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = "Invalid input(s).";
                return response;
            }

            try
            {
                // TODO: hash password.
                var user = new User
                {
                    Email = request.Email,
                    Password = request.Password,
                    Username = request.Username.ToLower(),
                    Fname = request.Fname,
                    Lname = request.Lname
                };

                if (await _usersRepository.Create(user))
                {
                    var createdUser = await _usersRepository.GetByUsername(user.Username);
                    var token = JwtHandler.GenerateToken(new JwtOptions
                    {
                        Issuer = _configuration["JwtSecurity:Issuer"],
                        Audience = _configuration["JwtSecurity:Audience"],
                        SecretKey = _configuration["JwtSecurity:SecretKey"],
                        PublicClaims = new Dictionary<string, string>()
                        {
                            { nameof(user.Username).ToLower(), user.Username},
                            { nameof(user.Email).ToLower(), user.Email},
                        },
                        Id = createdUser.Id,
                        Subject = "Authorization"
                    });

                    HttpContext.Response.Headers.Add("ACCESS-TOKEN", token.Token);
                    HttpContext.Response.Headers.Add("ACCESS-TOKEN-EXPIRATION", token.ValidTo.ToString());

                    response.Success = true;
                    response.Message = "User successfully created!";
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "Failed to create user.";
            }
           
            return response;
        }
        #endregion

        #region POST api/users/login

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<BaseResponse> Login([FromBody]LoginRequest request)
        {
            var response = new BaseResponse();

            try
            {
                var user = await _usersRepository.GetByUsername(request.Username.ToLower());
                if (user == null || request.Password != user.Password)
                {
                    HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    response.Message = "Unauthorized.";
                    return response;
                }

                var token = JwtHandler.GenerateToken(new JwtOptions
                {
                    Issuer = _configuration["JwtSecurity:Issuer"],
                    Audience = _configuration["JwtSecurity:Audience"],
                    SecretKey = _configuration["JwtSecurity:SecretKey"],
                    PublicClaims = new Dictionary<string, string>()
                    {
                        { nameof(user.Username).ToLower(), user.Username},
                        { nameof(user.Email).ToLower(), user.Email},
                    },
                    Id = user.Id,
                    Subject = "Authorization"
                });

                HttpContext.Response.Headers.Add("ACCESS-TOKEN", token.Token);
                HttpContext.Response.Headers.Add("ACCESS-TOKEN-EXPIRATION", token.ValidTo.ToString());

                response.Success = true;
                response.Message = "User logged in!";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "Failed to login.";
            }

            return response;
        }
        #endregion

        #region PUT api/users/logout

        [HttpPut("logout")]
        public BaseResponse Logout()
        {
            var response = new BaseResponse();

            try
            {
                HttpContext.Response.Headers.Remove("ACCESS-TOKEN");
                HttpContext.Response.Headers.Remove("ACCESS-TOKEN-EXPIRATION");

                response.Success = true;
                response.Message = "User logged out!";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "Failed to logout.";
            }

            return response;
        }
        #endregion

        #region GET api/users

        /// <summary>
        /// Get logged in user's information.
        /// </summary>
        /// <returns>The user's information.</returns>
        [HttpGet]
        public async Task<User> Get()
        {
            var users = (User)null;

            try
            {
                users = await _usersRepository.GetByUsername(HttpContext.User.GetUserUsername());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            return users;
        }
        #endregion
    }
}

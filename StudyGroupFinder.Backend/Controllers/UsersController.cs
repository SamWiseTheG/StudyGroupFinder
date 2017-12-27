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
using StudyGroupFinder.Backend.Utilities;
using StudyGroupFinder.Common.Models;
using StudyGroupFinder.Common.Requests;
using StudyGroupFinder.Common.Responses;
using StudyGroupFinder.Data.Repositories;

namespace StudyGroupFinder.Backend.Controllers
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

            try
            {
                var token = JwtHandler.GenerateToken(new JwtOptions
                {
                    Issuer = _configuration["JwtSecurity:Issuer"],
                    Audience = _configuration["JwtSecurity:Audience"],
                    SecretKey = _configuration["JwtSecurity:SecretKey"],
                    PublicClaims = new Dictionary<string, string>()
                    {
                        { nameof(request.Username).ToLower(), request.Username},
                    }
                });

                HttpContext.Response.Headers.Add("ACCESS-TOKEN", token.Token);
                HttpContext.Response.Headers.Add("ACCESS-TOKEN-EXPIRATION", token.ValidTo.ToString());

                await _usersRepository.Create(new User());

                response.Success = true;
                response.Message = "User successfully created!";
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
                var token = JwtHandler.GenerateToken(new JwtOptions
                {
                    Issuer = _configuration["JwtSecurity:Issuer"],
                    Audience = _configuration["JwtSecurity:Audience"],
                    SecretKey = _configuration["JwtSecurity:SecretKey"],
                    PublicClaims = new Dictionary<string, string>()
                    {
                    }
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

        #region PUT api/users/login

        [HttpPut("logout")]
        public async Task<BaseResponse> Logout()
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

        [HttpGet]
        public async Task<List<User>> Get()
        {
            var users = new List<User>();

            try
            {
                users = await _usersRepository.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                //response.Message = "Failed to create user.";
            }

            return users;
        }
        #endregion

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

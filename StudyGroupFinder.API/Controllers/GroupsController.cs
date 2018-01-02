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
    public class GroupsController : Controller
    {
        private IConfiguration _configuration;
        private GroupsRepository _groupsRepository;

        public GroupsController(IConfiguration configuration, GroupsRepository groupsRepository)
        {
            _configuration = configuration;
            _groupsRepository = groupsRepository;
        }

        #region POST api/groups/create
        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<BaseResponse> Create([FromBody]CreateGroupRequest request)
        {
            var response = new BaseResponse();

            if (request == null || !ModelState.IsValid)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = "Invalid input(s).";
                return response;
            }

            var group = new Group
            {
                Name = request.Name,
                Private = request.Private,
                Size = request.Size
            };

            try
            {

                if (await _groupsRepository.Create(group))
                {
                    var createdGroup = await _groupsRepository.GetByName(group.Name);
                    if( await _groupsRepository.AddUser(HttpContext.User.GetUserId().ToString(), createdGroup.Id.ToString())) {
                        
                        response.Success = true;
                        response.Message = "Group successfully created!";

                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                await _groupsRepository.Delete(group);
                Console.WriteLine(ex.Message);
                HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "Failed to create group.";
            }

            return response;
        }

        #endregion

        #region GET api/groups
        #endregion

        #region POST api/groups/request
        #endregion

        #region POST api/groups/invite
        [HttpPost("createinvite")]
        public async Task<BaseResponse> Invite([FromBody]GroupInviteRequest request)
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
                if (await _groupsRepository.CreateInvite(request.Group_Id, request.User_Id, request.Inviter_Id))
                {
                    response.Success = true;
                    response.Message = "User successfully invited to group!";
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
                response.Message = "Failed to create invite.";
            }

            return response;
        }
        #endregion


    }
}



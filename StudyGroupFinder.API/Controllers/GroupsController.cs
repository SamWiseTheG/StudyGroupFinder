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

        [HttpPost("create")]
        public async Task<BaseResponse> Create([FromBody]GroupCreateRequest request)
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
                Group createdGroup = null;

                if (await _groupsRepository.Create(group))
                {
                    createdGroup = await _groupsRepository.GetByName(group.Name);
                }
                else
                {
                    throw new Exception();
                }

                if(createdGroup != null) 
                {
                    if(await _groupsRepository.AddUser(HttpContext.User.GetUserId(), createdGroup.Id)) 
                    {
                        response.Message = "Group successfully created!";
                        response.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                try 
                {
                    await _groupsRepository.Delete(group);  // If the User addition fails, we need to delete the group we created.
                }
                catch {
                    Console.WriteLine(ex.Message);
                    HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    response.Message = "Failed to delete created group.";
                }

                Console.WriteLine(ex.Message);
                HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "Failed to create group.";
            }

            return response;
        }

        #endregion

        #region POST api/groups/join
        [HttpPost("join")]
        public async Task<BaseResponse> Join([FromBody]GroupJoinRequest request)
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
                var targetGroup = await _groupsRepository.GetById(request.Group_Id);
                var usersInGroup = await _groupsRepository.GetUsersInGroup(request.Group_Id);

                if(usersInGroup.Count() < targetGroup.Size) {
                    if (targetGroup.Private)
                    {
                        // TODO: handle checking for invite
                    }

                    else
                    {
                        if (await _groupsRepository.AddUser(HttpContext.User.GetUserId(), request.Group_Id))
                        {
                            response.Message = "Successfully joined group!";
                            response.Success = true;
                        }
                    }  
                }

                else 
                {
                    response.Message = "Join unsuccessful.  Group is full.";
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "Failed to join group.";
            }

            return response;
        }
        #endregion

        #region POST api/groups/invite
        [HttpPost("invite")]
        public async Task<BaseResponse> Invite([FromBody]GroupInviteRequest request)
        {
            var currentUserId = HttpContext.User.GetUserId();
            var response = new BaseResponse();

            if (request == null || !ModelState.IsValid)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = "Invalid input(s).";
                return response;
            }

            try 
            {
                if (!await _groupsRepository.UserInGroup(currentUserId, request.Group_Id)) 
                {
                    HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    response.Message = "User is not authorized to invite to this group.";
                    return response;
                }
            }
            catch 
            {
                HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                response.Message = "Failed to authorize invite rights.";
                return response;   
            }
  
            var invite = new GroupInvite
            {
                Group_Id = request.Group_Id,
                User_Id = currentUserId,
                Inviter_Id = request.Inviter_Id
            };

            try
            {
                if (await _groupsRepository.CreateInvite(invite))
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

        #region GET api/groups
        #endregion

        #region POST api/groups/request
        #endregion

    }
}



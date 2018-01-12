﻿using System;
using System.Threading.Tasks;
using StudyGroupFinder.Common.Models;
using Dapper;
using System.Collections.Generic;

namespace StudyGroupFinder.Data.Repositories
{
    public class GroupsRepository : BaseRepository
    {
        public GroupsRepository(DatabaseProvider databaseProvider) : base(databaseProvider)
        {
        }

        #region CREATE
        public async Task<bool> Create(Group group)
        {
            using (var conn = await _db.GetSqlConnection())
            {
                return (await conn.ExecuteAsync(@"
                    INSERT INTO `Groups`(Name, Size, Private) VALUES (@Name, @Size, @Private);",
                    group)) > 0;
            }
        }

        public async Task<bool> CreateRequest(int groupid, int userid)
        {
            using (var conn = await _db.GetSqlConnection())
            {
                return await conn.ExecuteAsync(@"
                    INSERT INTO `UserGroupRequests`(User_Id, Group_Id) VALUES (@userId, @groupId);", new { userId = userid, groupId = groupid }) > 0;
            }
        }

        public async Task<bool> CreateInvite(int groupid, int userid, int inviterid)
        {
            using (var conn = await _db.GetSqlConnection())
            {
                return await conn.ExecuteAsync(@"
                    INSERT INTO `UserGroupRequests`(User_Id, Group_Id, Inviter_Id) VALUES(@userId, @groupId, @inviterId);",
                    new { groupId = groupid, userId = userid, inviterId = inviterid }) > 0;
            }
        }

        public async Task<bool> CreateInvite(GroupInvite invite)
        {
            using (var conn = await _db.GetSqlConnection())
            {
                return await conn.ExecuteAsync(@"
                    INSERT INTO `UserGroupRequests`(User_Id, Group_Id, Inviter_Id) VALUES(@User_Id, @Group_Id, @Inviter_Id);",
                    invite) > 0;
            }
        }

        #endregion

        #region READ
        public async Task<List<Group>> GetAll()
        {
            using (var conn = await _db.GetSqlConnection())
            {
                return await conn.QueryAsync(@"
                    SELECT * FROM `Groups`") as List<Group>;
            }
        }

        public async Task<Group> GetById(int id)
        {
            using (var conn = await _db.GetSqlConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<Group>(@"
                    SELECT * FROM `Groups` WHERE Id = @Id",
                    new { Id = id });
            }
        }

        public async Task<Group> GetByName(string name)
        {
            using (var conn = await _db.GetSqlConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<Group>(@"
                    SELECT * FROM `Groups` WHERE Name = @Name",
                    new { Name = name });
            }
        }
        #endregion

        // TODO: GET LIKE NAME 

        #region UPDATE

        // Remove any pending invites or requests and add the user to the group
        public async Task<bool> AddUser(int userid, int groupid)
        {
            using (var conn = await _db.GetSqlConnection())
            {
                if (await conn.ExecuteAsync("DELETE FROM `UserGroupRequests` WHERE User_Id = '@userId' AND Group_Id = '@groupId';", new { userId = userid }) <= 0)
                {
                    return false;
                }
                if (await conn.ExecuteAsync("INSERT INTO `UserGroups`(User_Id, Group_Id) VALUES(@userId, @groupId);", new { userId = userid }) > 0)
                {
                    return true;
                }
                return false;
            }
        }

        #endregion

        #region DELETE

        public async Task<bool> Delete(Group group) 
        {
            using (var conn = await _db.GetSqlConnection())
            {
                return (await conn.ExecuteAsync(@"DELETE FROM `UserGroupRequests` WHERE Group_Id = '@Id';", group)) > 0;
            }
        }

        #endregion
    }
}

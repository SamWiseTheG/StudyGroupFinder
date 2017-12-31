﻿using System;
using System.Threading.Tasks;
using StudyGroupFinder.Common.Models;
using Dapper;
using System.Collections.Generic;

namespace StudyGroupFinder.Data.Repositories
{
    public class UsersRepository : BaseRepository
    {
        public UsersRepository(DatabaseProvider databaseProvider) : base(databaseProvider)
        {
        }

        #region CREATE
        public async Task<bool> Create(User user)
        {
            using (var conn = await _db.GetSqlConnection())
            {
                return (await conn.ExecuteAsync(@"
                    INSERT INTO `Students`(Username, Password, Email, Fname, Lname)
                    VALUES (@Username, @Password, @Email, @Fname, @Lname);",
                    user)) > 0;
            }
        }
        #endregion

        #region READ
        public async Task<List<User>> GetAll()
        {
            using (var conn = await _db.GetSqlConnection())
            {
                return await conn.QueryAsync(@"
                    SELECT * FROM `Students`") as List<User>;
            }
        }

        public async Task<User> GetById(int id)
        {
            using (var conn = await _db.GetSqlConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<User>(@"
                    SELECT * FROM `Students` WHERE Id = @Id", 
                    new { Id = id });
            }
        }

        public async Task<User> GetByUsername(string username)
        {
            using (var conn = await _db.GetSqlConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<User>(@"
                    SELECT * FROM `Students` WHERE Username = @Username",
                    new { Username = username });
            }
        }
        #endregion
    }
}

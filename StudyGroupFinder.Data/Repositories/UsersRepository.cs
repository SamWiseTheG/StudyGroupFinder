using System;
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
                    INSERT INTO Students(student_id, student_fname, student_lname)
                    VALUES (6, 'firstName', 'lastName');",
                    user)) > 0;
            }
        }
        #endregion

        #region GET
        public async Task<List<User>> GetAll()
        {
            using (var conn = await _db.GetSqlConnection())
            {
                return await conn.QueryAsync(@"
                    SELECT * FROM `Students`") as List<User>;
            }
        }

        public async Task<User> GetById(Guid id)
        {
            using (var conn = await _db.GetSqlConnection())
            {
                return await conn.QuerySingleOrDefaultAsync(@"
                    SELECT * FROM `Students` WHERE Id = @Id", 
                    new { Id = id });
            }
        }
        #endregion
    }
}

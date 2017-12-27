using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace StudyGroupFinder.Data
{
    public class DatabaseProvider
    {
        private string _connectionString;

        public DatabaseProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DatabaseProvider(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<MySqlConnection> GetSqlConnection()
        {
            try
            {
                var conn = new MySqlConnection(_connectionString);
                await conn.OpenAsync();
                return conn;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

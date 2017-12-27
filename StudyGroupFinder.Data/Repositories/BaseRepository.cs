using System;
namespace StudyGroupFinder.Data.Repositories
{
    public class BaseRepository
    {
        protected DatabaseProvider _db;
        public BaseRepository(DatabaseProvider databaseProvider)
        {
            _db = databaseProvider;
        }
    }
}

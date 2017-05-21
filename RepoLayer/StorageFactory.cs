using LiteDB;
using TaskManager.RepoLayer;

namespace RepoLayer
{
    public static class StorageFactory
    {
        public static string dbName;

        public static IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : Entity
        {
            return new NoSqlDb<TEntity>(new LiteDatabase(dbName));
        }
    }
}
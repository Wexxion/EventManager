using System.Data.Common;
using System.Data.Entity;
using TaskManager.RepoLayer.DataBase.DbModel;

namespace TaskManager.RepoLayer.DataBase
{
    public class Context : DbContext
    {
        public DbSet<PersonInDb> Users { get; set; }
        public DbSet<VEventInDb> Events { get; set; }

        public Context(string dbName)
            : base(dbName)
        {

        }

        public Context(DbConnection connection)
            : base(connection, true)
        {

        }
    }
}

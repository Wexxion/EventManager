using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace RepoLayer
{
    public class NoSqlDb<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        private LiteCollection<TEntity> collection;

        public NoSqlDb(LiteDatabase db)
        {
            collection = db.GetCollection<TEntity>();
        }
        public void Add(TEntity entity)
        {
            collection.Insert(entity);
        }
        public void Add(IEnumerable<TEntity> entitys)
        {
            collection.Insert(entitys);
        }

        public void Update(TEntity entity)
        {
            collection.Update(entity);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return collection.FindOne(predicate);
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            collection.Delete(predicate);
        }
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return collection.Find(predicate);
        }
        public IEnumerable<TEntity> GetAll()
        {
            return collection.FindAll();
        }
    }
}

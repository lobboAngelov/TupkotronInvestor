using System;
using System.Linq;
using Tupkach.Bot.Infrastructure.Entities.Interfaces;
namespace Tupkach.Bot.Infrastructure
{
    public interface IRepository
    {
        IQueryable<TEntity> GetAll<TEntity>() where TEntity : class;
        TEntity GetById<TEntity>(int id) where TEntity : class;
        void Insert<TEntity>(TEntity obj) where TEntity : class;
        void Delete<TEntity>(TEntity id) where TEntity : class;
        void Update<TEntity>(TEntity student) where TEntity : class;
        void Save();
    }

    public class TupkachRepository : IRepository
    {

        private readonly TupkachDbContext _dbContext;

        public TupkachRepository(TupkachDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return _dbContext.Set<TEntity>();
        }

        public TEntity GetById<TEntity>(int id) where TEntity : class
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public void Insert<TEntity>(TEntity obj) where TEntity : class
        {
            if (obj is ITrackable trackable)
            {
                trackable.Time = DateTime.Now;
            }
            _dbContext.Set<TEntity>().Add(obj);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void Update<TEntity>(TEntity student) where TEntity : class
        {
            _dbContext.Set<TEntity>().Add(student);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}

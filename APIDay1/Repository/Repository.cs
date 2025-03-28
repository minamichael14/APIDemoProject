using System.Linq.Expressions;
using APIDay1.Data;
using APIDay1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace APIDay1.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseModel
    {
        private readonly AppDbContext _appDbContext;
        DbSet<TEntity> _dbSet;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = _appDbContext.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
            SaveChanges();
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            SaveInclude(entity,nameof(entity.IsDeleted));
        }


        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.Where(x=> !x.IsDeleted);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression)
        {
            return GetAll().Where(expression);
        }

        public TEntity GetByID(int id)
        {
            return GetAll().Where(x=>x.ID == id).FirstOrDefault();
            
        }

        public bool IsExist(int id)
        {
           return _dbSet.Any(x=>x.ID == id && !x.IsDeleted);
        }

        public void SaveChanges()
        {
            _appDbContext.SaveChanges();
        }

        public void SaveInclude(TEntity entity, params string[] properties)
        {
            var local = _dbSet.Local.FirstOrDefault(x => x.ID == entity.ID);

            EntityEntry entityEntry = null;

            if (local is null)
            {
                entityEntry = _appDbContext.Entry(entity);
            }
            else
            {
                entityEntry = _appDbContext.ChangeTracker
                    .Entries<TEntity>().FirstOrDefault(x => x.Entity.ID == entity.ID);
            }

            foreach (var property in entityEntry.Properties)
            {
                if (properties.Contains(property.Metadata.Name))
                {
                    property.CurrentValue = entity.GetType().GetProperty(property.Metadata.Name).GetValue(entity);
                    property.IsModified = true;
                }
            }


        }
    }
}

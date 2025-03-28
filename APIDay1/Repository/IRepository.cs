using System.Linq.Expressions;
using APIDay1.Models;

namespace APIDay1.Repository
{
    public interface IRepository<TEntity> where TEntity : BaseModel
    {
        // Retriving 
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression);
        TEntity GetByID(int id);

        // Add
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);


        // Update 
        void SaveInclude(TEntity entity, params string[] properties);

        //Delete
        void Delete(TEntity entity);

        // IsExist
        bool IsExist(int id);

        // SaveChanges
        void SaveChanges();
    }
}

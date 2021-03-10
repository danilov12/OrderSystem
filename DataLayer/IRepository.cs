using Domain;
using System.Collections.Generic;

namespace DataLayer
{
    public interface IRepository<T> where T : IEntity
    {
        void AddEntity(T entity);
        T GetEntityById(int id);
        IEnumerable<T> GetAllEntities();
        void DeleteEntity(int id);
        void UpdateEntity(T entity);
    }
}

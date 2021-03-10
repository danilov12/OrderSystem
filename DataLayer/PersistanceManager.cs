using Domain;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataLayer
{
    public class PersistanceManager<T> : IRepository<T> where T : IEntity
    {
        private string _path;

        public PersistanceManager(string path)
        {
            this._path = path;
        }

        public void AddEntity(T entity)
        {
            IEnumerable<T> entites =  this.GetAllEntities() != null ? this.GetAllEntities() : new List<T>();
            entity.Id = this.GetNextId(entites);
            entites = entites.Append(entity);

            this.WriteToJsonFile(entites);
        }

        public void DeleteEntity(int id)
        {
            IEnumerable<T> entites = this.GetAllEntities();
            this.WriteToJsonFile(this.ReturnCollectionWithoutDeletedEntity(entites, id));
        }

        public IEnumerable<T> GetAllEntities()
        {
            List<T> entites = new List<T>();
            using (StreamReader streamReader = new StreamReader(this._path))
            {
                string json = streamReader.ReadToEnd();
                entites = JsonConvert.DeserializeObject<List<T>>(json);
            }

            return entites;
        }

        public T GetEntityById(int id)
        {
            IEnumerable<T> entities = this.GetAllEntities();

            return entities
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public void UpdateEntity(T entity)
        {
            IEnumerable<T> entities = this.GetAllEntities();
            List<T> updatedEntities = new List<T>();

            foreach(T entityInList in entities)
            {
                if (entityInList.Id == entity.Id)
                {
                    updatedEntities.Add(entity);
                }
                else
                {
                    updatedEntities.Add(entityInList);
                }
            }

            this.WriteToJsonFile(updatedEntities);
        }

        #region HelperMethod
        private void WriteToJsonFile(IEnumerable<T> entities)
        {
            string json = JsonConvert.SerializeObject(entities);

            using (StreamWriter streamWriter = new StreamWriter(this._path))
            {
                streamWriter.Write(json);
            }
        }

        private IEnumerable<T> ReturnCollectionWithoutDeletedEntity(IEnumerable<T> entities, int id)
        {
            return entities
                .Where(x => x.Id != id);
        }

        private int GetNextId(IEnumerable<T> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                return entities.Max(x => x.Id) + 1;
            }
            return 1;
        }
        #endregion
    }
}

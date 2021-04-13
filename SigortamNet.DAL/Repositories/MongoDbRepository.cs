using MongoDB.Bson;
using MongoDB.Driver;
using SigortamNet.Core.Entities.Base;
using SigortamNet.DAL.Abstract;
using SigortamNet.DAL.Settings;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Tusky.DAL.Base
{
    public class MongoDbRepository<T> : IRepository<T> where T : Entity, IEntity, new()
    {


        public IMongoCollection<T> Collection { get; private set; }
        public MongoDbRepository()
        {
            MongoClient _dbContext = new MongoClient(DataAccessSettings.DefaultDbConnectionString);
            string _database = DataAccessSettings.DefaultDBName;
            Collection = _dbContext.GetDatabase(_database).GetCollection<T>(typeof(T).Name);
        }


        public T GetById(string id)
        {
            ObjectId objectId;
            if (!ObjectId.TryParse(id.ToString(), out objectId))
            {
                return null;
            }
            var filterId = Builders<T>.Filter.Eq("_id", objectId);
            T model;

            model = Collection.Find(filterId).FirstOrDefault();

            return model;
        }

        public bool Update(T entity)
        {
           
            ObjectId objectId;
            if (!ObjectId.TryParse(entity.Id.ToString(), out objectId))
            {
                throw new InvalidOperationException();
            }

            var filterId = Builders<T>.Filter.Eq("_id", objectId);
            T updated;

            updated = Collection.FindOneAndReplace(filterId, entity);
            return updated != null;
        }


        public void Insert(T entity)
        {

            Collection.InsertOne(entity);
        }


        public bool Delete(string id)
        {
            ObjectId objectId;
            if (!ObjectId.TryParse(id.ToString(), out objectId))
            {
                return false;
            }
            var filterId = Builders<T>.Filter.Eq("_id", objectId);
            T deleted;

            deleted = Collection.FindOneAndDelete(filterId);
            return deleted != null;
        }



        public IEnumerable<T> Query()
        {
            return Collection.Find(FilterDefinition<T>.Empty).ToList();
        }


        public IEnumerable<T> Query(Expression<Func<T, bool>> filter)
        {
            return Collection.Find(filter).ToList();
        }

    }
}

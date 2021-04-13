
using SigortamNet.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace SigortamNet.DAL.Abstract
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        T GetById(string id);

        bool Update(T entity);

        void Insert(T entity);

        bool Delete(string id);

        IEnumerable<T> Query();

        IEnumerable<T> Query(Expression<Func<T, bool>> filter);

    }
}

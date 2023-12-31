using Core.Entities;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        T GetById(int id);
        IList<T> GetAll();
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
        long GetCount();
    }
}
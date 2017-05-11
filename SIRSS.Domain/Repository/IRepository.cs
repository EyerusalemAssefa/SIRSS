using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SIRSS.Domain.Repository
{
    public interface IRepository<T> where T : class
    {
        long Count(Expression<Func<T, bool>> predicate);

        bool Delete(T entity);

        bool Delete(IEnumerable<T> entities);

        bool Delete(Expression<Func<T, bool>> predicate);

        bool DeleteById(long id);
        
        void Edit(T entity);

        bool Exists(Expression<Func<T, bool>> predicate);

        IEnumerable<T> GetAll();

        T GetById(long id);

        bool Insert(T entity);

        bool Insert(IEnumerable<T> entities);

        bool InsertOrUpdate(IEnumerable<T> entities);

        bool InsertOrUpdate(T entity);

        T SelectSingle(Expression<Func<T, bool>> predicate);

        IEnumerable<T> Select(Expression<Func<T, bool>> predicate);

        IEnumerable<T> Search(Expression<Func<T, bool>> predicate, int top);

        void Detach(T entity);
    }
}
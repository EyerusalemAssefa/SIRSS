using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

namespace SIRSS.Domain.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public DbSet<T> Dbset;
        public readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
            Dbset = context.Set<T>();
        }

        public long Count(Expression<Func<T, bool>> predicate)
        {
            return Dbset.Count(predicate);
        }

        public bool Delete(T entity)
        {
            if (entity == null) return false;

            if (Context.Entry(entity).State == EntityState.Detached)
            {
                Dbset.Attach(entity);
            }

            Dbset.Remove(entity);

            return true;
        }

        public bool Delete(IEnumerable<T> entities)
        {
            return entities.All(Delete);
        }

        public bool Delete(Expression<Func<T, bool>> predicate)
        {
            var entity = Dbset.Where(predicate);
            return entity.All(Delete);
        }

        public bool DeleteById(long id)
        {
            return Delete(GetById(id));
        }

        public void Edit(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return Dbset.Any(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return Dbset.ToList();
        }

        public T GetById(long id)
        {
            return Dbset.Find(id);
        }

        public bool Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    //throw new ArgumentNullException(nameof(entity));
                    return false;
                }

                Dbset.Add(entity);

                return true;
            }
            catch (DbEntityValidationException)
            {
                //foreach (var validationError in dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors))
                //{
                //    _errorMessage +=
                //        $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}" + Environment.NewLine;
                //}
                return false;
                //throw new Exception(_errorMessage, dbEx);
            }
        }

        public bool Insert(IEnumerable<T> entities)
        {
            return entities.All(Insert);
        }

        public bool InsertOrUpdate(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                Dbset.AddOrUpdate(entity);

                return true;
            }
            catch (DbEntityValidationException)
            {
                //foreach (var validationError in dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors))
                //{
                //    _errorMessage +=
                //        $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}" + Environment.NewLine;
                //}
                return false;
                //throw new Exception(_errorMessage, dbEx);
            }
        }

        public bool InsertOrUpdate(IEnumerable<T> entities)
        {
            return entities.All(InsertOrUpdate);
        }

        public IEnumerable<T> Select(Expression<Func<T, bool>> predicate)
        {
            return Dbset.Where(predicate).AsEnumerable();
        }

        public T SelectSingle(Expression<Func<T, bool>> predicate)
        {
            return Dbset.Where(predicate).FirstOrDefault();
        }

        public IEnumerable<T> Search(Expression<Func<T, bool>> predicate, int top)
        {
            return top == 0 ? Dbset.Where(predicate).AsEnumerable() :
                              Dbset.Where(predicate).Take(top);
        }

        public void Detach(T entity)
        {
            Context.Entry(entity).State = EntityState.Detached;
        }
    }
}
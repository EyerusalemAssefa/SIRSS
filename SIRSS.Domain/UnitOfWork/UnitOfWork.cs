using System;
using System.Collections.Generic;
using SIRSS.Domain.Infrastructure;
using SIRSS.Domain.Repository;

namespace SIRSS.Domain.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private bool _disposed;
        private readonly SirssContext _context;
        private Dictionary<string, object> _repositories = new Dictionary<string, object>();

        public UnitOfWork()
        {
            _context = new SirssContext();
        }

        public UnitOfWork(SirssContext context)
        {
            _context = context;
        }

        public bool Save()
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IRepository<T> Repository<T>() where T : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;

            if (_repositories.ContainsKey(type)) return (Repository<T>) _repositories[type];

            var repositoryType = typeof(Repository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
            _repositories.Add(type, repositoryInstance);

            return (Repository<T>)_repositories[type];
        }
    }

}

using System;
using SIRSS.Domain.Repository;

namespace SIRSS.Domain.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        bool Save();

        IRepository<T> Repository<T>() where T : class;
    }
}
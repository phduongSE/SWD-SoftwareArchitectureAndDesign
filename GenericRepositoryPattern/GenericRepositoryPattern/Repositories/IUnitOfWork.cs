﻿namespace GenericRepositoryPattern.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class;

        void SaveChanges();
    }
}
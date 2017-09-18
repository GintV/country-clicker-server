using CountryClicker.Data;
using CountryClicker.Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;

namespace CountryClicker.DataService
{
    public interface IDataService<TEntity, TIdentifier>
        where TEntity : IEntity
        where TIdentifier : IComparable
    {
        EntityEntry Create(TEntity instance);
        void CreateMany(TEntity[] instances);
        EntityEntry Delete(TEntity instance);
        void DeleteMany(TEntity[] instances);
        bool Exists(TIdentifier id);
        TEntity Get(TIdentifier id);
        IEnumerable<TEntity> GetMany();
        int SaveChanges();
        EntityEntry Update(TEntity instance);
        void UpdateMany(TEntity[] instances);
    }

    public abstract class DataService<TEntity, TIdentifier> : IDataService<TEntity, TIdentifier>
        where TEntity : IEntity
        where TIdentifier : IComparable
    {
        protected CountryClickerDbContext m_context;

        public DataService(CountryClickerDbContext context)
        {
            m_context = context;
        }

        public EntityEntry Create(TEntity instance) => m_context.Add((object)instance);
        public EntityEntry Delete(TEntity instance) => m_context.Remove((object)instance);
        public bool Exists(TIdentifier id) => Get(id) != null;
        public EntityEntry Update(TEntity instance) => m_context.Update((object)instance);
        public int SaveChanges() => m_context.SaveChanges();

        public abstract void CreateMany(TEntity[] instances);
        public abstract void DeleteMany(TEntity[] instances);
        public abstract TEntity Get(TIdentifier id);
        public abstract IEnumerable<TEntity> GetMany();
        public abstract void UpdateMany(TEntity[] instances);
    }
}
